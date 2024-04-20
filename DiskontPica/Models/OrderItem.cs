using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiskontPica.Models
{
	public class OrderItem
	{
		[ForeignKey("Order")]
		public int orderId { get; set; }
		[Key]
		public int orderItemId { get; set; }

		[ForeignKey("Product")]
		public int productId { get; set; }
		public int quantity { get; set; }
		public decimal priceQuantity { get; set; }

	}

	
}
