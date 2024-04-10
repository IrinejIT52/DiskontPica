using DiskontPica.Helper;
using DiskontPica.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DiskontPica.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }


        [HttpPost("admin")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AuthenticateAdmin(Principal principal)
        {
            var admin = authenticationHelper.AuthenticatePrincipalAdmin(principal);
            
            if (admin != null)
            {
                var tokenString = authenticationHelper.GenerateJwtAdmin(admin);
                return Ok(new { token = tokenString });
            }


            //Ukoliko autentifikacija nije uspela vraća se status 401
            return Unauthorized();
        }

		[HttpPost("customer")]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public IActionResult AuthenticateCustomer(Principal principal)
		{

			var customer = authenticationHelper.AuthenticatePrincipalCustomer(principal);
			if (customer != null)
			{
				var tokenString = authenticationHelper.GenerateJwtCustomer(customer);
				return Ok(new { token = tokenString });
			}

			//Ukoliko autentifikacija nije uspela vraća se status 401
			return Unauthorized();
		}

	}
}
