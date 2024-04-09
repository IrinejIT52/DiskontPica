using System.ComponentModel.DataAnnotations;

namespace DiskontPica.Models
{
	public class Customer
	{
		[Key]
		public int customerld {  get; set; }
		public string name { get; set; }
		public string email { get; set; }
		public string adress { get; set; }

	}
}
