using DiskontPica.Models;
using DiskontPica.Repository;

namespace DiskontPica.Helper
{
	public interface IAuthenticationHelper
	{
		public Administrator AuthenticatePrincipalAdmin(Principal principal);

		public string GenerateJwtAdmin(Administrator principal);

		public Customer AuthenticatePrincipalCustomer(Principal principal);

		public string GenerateJwtCustomer(Customer principal);
	}
}
