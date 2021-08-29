using System;
using car_rent_backend.repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace car_rent_backend.Tests.integration.setup
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (AppDbContext) using an in-memory database for testing.
                services.AddSingleton(
                    new DbContextOptionsBuilder<ProjectContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options);
            });
        }

        public void SetupDb(DbContextOptions<ProjectContext> options)
        {
            using var context = new ProjectContext(options);

            context.Cars.RemoveRange(context.Cars);
            context.Customers.RemoveRange(context.Customers);
            context.Reservations.RemoveRange(context.Reservations);

            SeedData.Populate(context);

            context.SaveChanges();
        }
    }
}
