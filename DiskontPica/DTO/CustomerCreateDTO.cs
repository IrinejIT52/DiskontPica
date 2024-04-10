using System.ComponentModel.DataAnnotations;

namespace DiskontPica.DTO
{
	public class CustomerCreateDTO
	{
		[Required(ErrorMessage = "Username is required")]
		public string name { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string password { get; set; }
		[Required(ErrorMessage = "Email is required")]
		public string email { get; set; }
		[Required(ErrorMessage = "Adress is required")]
		public string adress { get; set; }
	}
}
