using DiskontPica.Models;
using DiskontPica.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiskontPica.Helper
{
	public class AuthenticationHelper : IAuthenticationHelper
	{

		private readonly IConfiguration _configuration;
		private readonly IDrinkStoreRepository _drinkStoreRepository;

		public AuthenticationHelper(IConfiguration configuration, IDrinkStoreRepository drinkStoreRepository)
		{
			_configuration = configuration;
			_drinkStoreRepository = drinkStoreRepository;
		}


		public Administrator AuthenticatePrincipalAdmin(Principal principal)
		{
			var admin = _drinkStoreRepository.GetAdministratorWithCredentials(principal.name, principal.password);

			return admin;
		}

		public Customer AuthenticatePrincipalCustomer(Principal principal)
		{
			var customer = _drinkStoreRepository.GetCustomerWithCredentials(principal.name, principal.password);

			return customer;
		}

		public string GenerateJwtAdmin(Administrator principal)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var claims = new List<Claim>
			{
				new Claim("name", principal.name),
				new Claim("email", principal.email),
				new Claim("admin", "True"),
				new Claim("customer", "True")
			};


			var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
											 _configuration["Jwt:Issuer"],
											 claims,
											 expires: DateTime.Now.AddMinutes(120),
											 signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public string GenerateJwtCustomer(Customer principal)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var claims = new List<Claim>
			{
				new Claim("name", principal.name),
				new Claim("email", principal.email),
				new Claim("adress", principal.adress),
				new Claim("customer","True")
			};


			var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
											 _configuration["Jwt:Issuer"],
											 claims,
											 expires: DateTime.Now.AddMinutes(120),
											 signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
