using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

		[ForeignKey("Country")]
		public int countryId { get; set; }

		[ForeignKey("Category")]
		public int categoryId { get; set; }

		[ForeignKey("Administrator")]
		public int adminId { get; set; }

	}
}
