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

		private readonly IConfiguration configuration;
		private readonly IDrinkStoreRepository drinkStoreRepository;

		public AuthenticationHelper(IConfiguration configuration, IDrinkStoreRepository userRepository)
		{
			this.configuration = configuration;
			this.drinkStoreRepository = drinkStoreRepository;
		}


		public Administrator AuthenticatePrincipalAdmin(Principal principal)
		{
			var user = drinkStoreRepository.GetAdministratorWithCredentials(principal.name, principal.password);

			return user;
		}

		public Customer AuthenticatePrincipalCustomer(Principal principal)
		{
			var user = drinkStoreRepository.GetCustomerWithCredentials(principal.name, principal.password);

			return user;
		}

		public string GenerateJwtAdmin(Administrator principal)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var claims = new List<Claim>
			{
				new Claim("name", principal.name),
				new Claim("email", principal.email),
			};


			var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
											 configuration["Jwt:Issuer"],
											 claims,
											 expires: DateTime.Now.AddMinutes(120),
											 signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public string GenerateJwtCustomer(Customer principal)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var claims = new List<Claim>
			{
				new Claim("name", principal.name),
				new Claim("email", principal.email),
			};


			var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
											 configuration["Jwt:Issuer"],
											 claims,
											 expires: DateTime.Now.AddMinutes(120),
											 signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
