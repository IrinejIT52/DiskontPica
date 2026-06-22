using DiskontPica.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
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

		

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				var connectionString = configuration.GetConnectionString("DB");
				optionsBuilder.UseNpgsql(connectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderItem>().ToTable(tb => tb.HasTrigger("trg_DecreaseStock"));

			// The PostgreSQL database stores orderStatus and orderType as VARCHAR(50)
			// (e.g. "PENDING", "REGULAR") instead of integers.
			// HasConversion<string>() tells EF Core to serialize/deserialize the enum
			// as its name string, matching the actual column type.
			modelBuilder.Entity<Order>()
				.Property(o => o.orderStatus)
				.HasConversion<string>();

			modelBuilder.Entity<Order>()
				.Property(o => o.orderType)
				.HasConversion<string>();
		}
	}
}
