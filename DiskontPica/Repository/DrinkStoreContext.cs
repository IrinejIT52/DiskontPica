using DiskontPica.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Npgsql.NameTranslation;
using System.Security;

namespace DiskontPica.Repository
{
	public class DrinkStoreContext : DbContext
	{
		private readonly IConfiguration configuration;

		public DrinkStoreContext(DbContextOptions<DrinkStoreContext> options, IConfiguration configuration) : base(options)
		{
			this.configuration = configuration;
		}

		

		public DbSet<Administrator> Administrator { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<Country> Country { get; set; }	
		public DbSet<Customer> Customer { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }

		

		// Preserves C# enum member names as-is so they match the uppercase
		// PG enum labels (e.g. PENDING, REGULAR) stored in Supabase.
		private sealed class IdentityNameTranslator : INpgsqlNameTranslator
		{
			public static readonly IdentityNameTranslator Instance = new();
			public string TranslateMemberName(string clrName) => clrName;
			public string TranslateTypeName(string clrName) => clrName;
		}

		// Static data source with Npgsql enum mappings registered.
		// PostgreSQL native enum types require this so Npgsql sends the
		// enum value as the correct PG type instead of plain text.
		private static NpgsqlDataSource? _dataSource;

		private NpgsqlDataSource GetOrCreateDataSource()
		{
			if (_dataSource == null)
			{
				var connectionString = configuration.GetConnectionString("DB");
				var builder = new NpgsqlDataSourceBuilder(connectionString);
				builder.MapEnum<OrderStatus>("orderStatus", IdentityNameTranslator.Instance);
				builder.MapEnum<OrderType>("orderType", IdentityNameTranslator.Instance);
				_dataSource = builder.Build();
			}
			return _dataSource;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseNpgsql(GetOrCreateDataSource());
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderItem>().ToTable(tb => tb.HasTrigger("trg_DecreaseStock"));

			// orderStatus and orderType are PostgreSQL native enum types in Supabase.
			// We tell EF Core the exact PG column type so it matches the enum registration
			// done in NpgsqlDataSourceBuilder.MapEnum above.
			modelBuilder.Entity<Order>()
				.Property(o => o.orderStatus)
				.HasColumnType("orderStatus");

			modelBuilder.Entity<Order>()
				.Property(o => o.orderType)
				.HasColumnType("orderType");
		}
	}
}
