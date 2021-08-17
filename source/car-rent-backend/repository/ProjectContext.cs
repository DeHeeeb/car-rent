using System.Collections.Generic;
using car_rent_backend.domain;
using Microsoft.EntityFrameworkCore;

namespace car_rent_backend.repository
{
    public class ProjectContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Customer> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Database=CarRent; Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Car>()
                .Property(c => c.Type)
                .HasConversion<string>();

            modelBuilder
                .Entity<Car>()
                .Property(c => c.Class)
                .HasConversion<string>();

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

            var cars = new List<Car>
            {
                new()
                {
                    Id = 1,
                    CarNr = "1001",
                    Type = CarType.Limousine,
                    Class = CarClass.Low,
                    Brand = "Peugeot"
                },
                new()
                {
                    Id = 2,
                    CarNr = "1015",
                    Type = CarType.Minivan,
                    Class = CarClass.Medium,
                    Brand = "Citroen"
                },
                new()
                {
                    Id = 3,
                    CarNr = "5075",
                    Type = CarType.Convertible,
                    Class = CarClass.High,
                    Brand = "Chevrolet"
                },
                new()
                {
                    Id = 4,
                    CarNr = "1043",
                    Type = CarType.Limousine,
                    Class = CarClass.Medium,
                    Brand = "VW"
                },
                new()
                {
                    Id = 5,
                    CarNr = "9311",
                    Type = CarType.Convertible,
                    Class = CarClass.Medium,
                    Brand = "BMW"
                },
                new()
                {
                    Id = 6,
                    CarNr = "1353",
                    Type = CarType.Minivan,
                    Class = CarClass.High,
                    Brand = "Suzuki"
                },
                new()
                {
                    Id = 7,
                    CarNr = "3197",
                    Type = CarType.Limousine,
                    Class = CarClass.High,
                    Brand = "Tesla"
                },
                new()
                {
                    Id = 8,
                    CarNr = "4220",
                    Type = CarType.Minivan,
                    Class = CarClass.Medium,
                    Brand = "VW"
                },
                new()
                {
                    Id = 9,
                    CarNr = "8314",
                    Type = CarType.Limousine,
                    Class = CarClass.Low,
                    Brand = "VW"
                },
                new()
                {
                    Id = 10,
                    CarNr = "4750",
                    Type = CarType.Limousine,
                    Class = CarClass.Low,
                    Brand = "VW"
                },
            };

            customers.ForEach(customer => modelBuilder.Entity<Customer>().HasData(customer));
            cars.ForEach(car => modelBuilder.Entity<Car>().HasData(car));
        }
    }
}
