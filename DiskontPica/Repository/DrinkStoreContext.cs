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

		public DbSet<Administrator> Administrator { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<Country> Country { get; set; }	
		public DbSet<Customer> Customer { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=IRINEJ\\SQLEXPRESS;Initial Catalog=DrinkStore;Integrated Security=True;Encrypt=False");
		}
	}
}
