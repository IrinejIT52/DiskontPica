using System.ComponentModel.DataAnnotations;

namespace DiskontPica.DTO
{
	public class AdministratorCreateDTO
	{
		[Required(ErrorMessage = "Username is required")]
		public string name { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string password { get; set; }

		[Required(ErrorMessage = "Email is required")]
		public string email { get; set; }

	}
}
