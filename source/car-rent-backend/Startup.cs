using car_rent_backend.repository;
using car_rent_backend.service;
using car_rent_backend.service.validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace car_rent_backend
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

            services.AddTransient<CarService>();
            services.AddTransient<CustomerService>();
            services.AddTransient<ReservationService>();

            services.AddTransient<CarRepository>();
            services.AddTransient<CustomerRepository>();
            services.AddTransient<ReservationRepository>();

            services.AddTransient<CarValidationService>();
            services.AddTransient<CustomerValidationService>();
            services.AddTransient<ReservationValidationService>();

            services.AddSingleton(
                new DbContextOptionsBuilder<ProjectContext>()
                .UseSqlServer("Data Source=.; Database=CarRent; Trusted_Connection=True")
                .Options);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
        }
    }
}
