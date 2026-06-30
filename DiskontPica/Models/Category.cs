using System.ComponentModel.DataAnnotations;

namespace DiskontPica.Models
{
	public class Category
	{
		[Key]
		public int categoryId {  get; set; }
		public int? superCategoryId { get; set; }
		public string name { get; set; }

		public string description { get; set; }

	}
}
