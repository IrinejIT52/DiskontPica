using System.ComponentModel.DataAnnotations;

namespace DiskontPica.Models
{
	public class Administrator
	{
		[Key]
		public int adminId { get; set; }
		public string name { get; set; }

		public string email { get; set; }
	}
}
