using DiskontPica.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskontPica.Repository
{
	public class DrinkStoreContext : DbContext
	{
		private readonly IConfiguration configuration;

		public DrinkStoreContext(DbContextOptions<DrinkStoreContext> options, IConfiguration configuration) : base(options)
		{
			this.configuration = configuration;
		}

		public DbSet<Administrator> Administrators { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Country> Countries { get; set; }	
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=IRINEJ\\SQLEXPRESS;Initial Catalog=DrinkStore;Integrated Security=True;Encrypt=False");
		}
	}
}
