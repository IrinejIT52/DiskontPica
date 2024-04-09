using System.ComponentModel.DataAnnotations;

namespace DiskontPica.Models
{
	public class Order
	{
		[Key]
		public int orderId {  get; set; }
		public Customer customer { get; set; } = new Customer();
		public decimal finalPrice { get; set; }
		public DateOnly orderDate {  get; set; }
		public OrderStatus orderStatus { get; set; }
		public OrderType orderType { get; set; }
		public string addiitionalInfo { get; set; }
	}
}
