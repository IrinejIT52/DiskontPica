using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiskontPica.Models
{
	public class Order
	{
		[Key]
		public int orderId {  get; set; }
		[ForeignKey("Customer")]
		public int customerId { get; set; }
		[NotMapped]
		public List<OrderItem>orderItems { get; set; }
		public decimal finalPrice { get; set; }
		public DateOnly orderDate {  get; set; }
		public OrderStatus orderStatus { get; set; }
		public OrderType orderType { get; set; }
		public string addiitionalInfo { get; set; }
	}
}
