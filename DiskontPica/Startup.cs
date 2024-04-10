using DiskontPica.Helper;
using DiskontPica.Profiles;
using DiskontPica.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace DiskontPica
{
	public class Startup
	{
		public IConfiguration _configuration { get; }

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddScoped<IDrinkStoreRepository, DrinkStoreRepository>();
			services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
			services.AddDbContext<DrinkStoreContext>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddAutoMapper(typeof(AdministratorProfile).Assembly);
			services.AddAutoMapper(typeof(CustomerProfile).Assembly);
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = _configuration["Jwt:Issuer"],
					ValidAudience = _configuration["Jwt:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
				};
			});



			services.AddSwaggerGen(c =>
			{ 
			});
		}


			public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
			{
				if (env.IsDevelopment())
				{
					app.UseDeveloperExceptionPage();
				}


		

				app.UseHttpsRedirection();

				app.UseRouting();

				app.UseAuthorization();

				app.UseEndpoints(endpoints =>
				{
					endpoints.MapControllers();
				});

				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "DrinkStore");
					options.RoutePrefix = string.Empty;
				});







			}
		}

	}
