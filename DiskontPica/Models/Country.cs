using System.ComponentModel.DataAnnotations;

namespace DiskontPica.Models
{
	public class Country
	{
		[Key]
		public int countryId { get; set; }

		public string name { get; set; }
	}
}
