using DiskontPica.Repository;

namespace DiskontPica
{
	public class Startup
	{

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddScoped<IDrinkStoreRepository, DrinkStoreRepository>();
			services.AddDbContext<DrinkStoreContext>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services.AddSwaggerGen(options =>
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
