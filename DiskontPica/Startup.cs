﻿using DiskontPica.Helper;
using DiskontPica.Models;
using DiskontPica.Profiles;
using DiskontPica.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
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
			services.Configure<StripeSettings>(_configuration.GetSection("StripeSettings"));
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

			services.AddAuthorization(options =>
			{
				options.AddPolicy(IdentityData.AdminPolicy, p => p.RequireClaim(IdentityData.AdminClaim,"True"));
				options.AddPolicy(IdentityData.CustomerPolicy,p => p.RequireClaim(IdentityData.CustomerClaim,"True"));
			});

			services.AddCors(
				options =>
				{
					options.AddPolicy(
					"AllowCors",
						builder =>
						{
							builder.AllowAnyOrigin().WithMethods(
								HttpMethod.Get.Method,
								HttpMethod.Put.Method,
								HttpMethod.Post.Method,
								HttpMethod.Delete.Method).
								AllowAnyHeader();
						});
				});



			services.AddSwaggerGen(c =>
			{
				c.OperationFilter<HeaderFIlter>();
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header. \r\n\r\n Enter the token in the text input below.",
				});
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

				app.UseCors("AllowCors");

				app.UseHttpsRedirection();

				app.UseAuthentication();

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
