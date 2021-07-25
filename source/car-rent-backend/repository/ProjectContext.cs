using System.Collections.Generic;
using car_rent_backend.domain;
using Microsoft.EntityFrameworkCore;

namespace car_rent_backend.repository
{
    public class ProjectContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Database=CarRent; Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var customers = new List<Customer>
            {
                new()
                {
                    Id = 1,
                    CustomerNr = "C123",
                    LastName = "Meier",
                    FirstName = "Hans",
                    Street = "Turmstrasse 3",
                    Zip = 9000,
                    City = "St. Gallen"
                },
                new ()
                {
                    Id = 2,
                    CustomerNr = "C463",
                    LastName = "Hugentobler",
                    FirstName = "Jasmin",
                    Street = "Im Tobel 19",
                    Zip = 9500,
                    City = "Wil SG"
                },
                new ()
                {
                    Id = 3,
                    CustomerNr = "C932",
                    LastName = "Manser",
                    FirstName = "Reto",
                    Street = "Bühlstrasse 18a",
                    Zip = 9050,
                    City = "Appenzell"
                },
                new ()
                {
                    Id = 4,
                    CustomerNr = "C435",
                    LastName = "Heeb",
                    FirstName = "Lukas",
                    Street = "Hauptstrasse 50",
                    Zip = 9000,
                    City = "St. Gallen"
                },
                new ()
                {
                    Id = 5,
                    CustomerNr = "N773",
                    LastName = "Muster",
                    FirstName = "Hanna",
                    Street = "Wurstweg 5",
                    Zip = 1000,
                    City = "Lausanne"
                },
            };

            customers.ForEach(customer => modelBuilder.Entity<Customer>().HasData(customer));
        }
    }
}
