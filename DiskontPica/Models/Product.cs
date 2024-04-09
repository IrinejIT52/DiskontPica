using System.ComponentModel.DataAnnotations;

namespace DiskontPica.Models
{
	public class Product
	{
		[Key]
		public int productId {  get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public decimal price { get; set; }
		public int stock { get; set; }

		public Country country { get; set; } = new Country();
		public Category category { get; set; } = new Category();

		public Administrator administrator { get; set; } = new Administrator();

	}
}
