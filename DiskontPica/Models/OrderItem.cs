using System.ComponentModel.DataAnnotations;

namespace DiskontPica.Models
{
	public class OrderItem
	{
		public Order order { get; set; } = new Order();
		[Key]
		public int orderItemId { get; set; }
		public Product product { get; set; } = new Product();
		public int quantity { get; set; }
		public decimal priceQuantity { get; set; }

	}
}
