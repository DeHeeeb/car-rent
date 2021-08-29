using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using car_rent_backend.domain;
using car_rent_backend.repository;
using car_rent_backend.Tests.integration.setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace car_rent_backend.Tests.integration
{
    public class CarIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CarIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            var options = factory.Services.GetRequiredService<DbContextOptions<ProjectContext>>();
            factory.SetupDb(options);
        }

        [Fact]
        public async Task GivenListOfCars_WhenGetAll_ThenListIsReturned()
        {
            var response = await _client.GetAsync("car");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<IEnumerable<Car>>(stringResponse);
            Assert.Equal(10, cars.Count());
        }

        [Fact]
        public async Task GivenListOfCars_WhenGetId2_ThenCarWithId2Returned()
        {
            var response = await _client.GetAsync("car/2");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var car = JsonConvert.DeserializeObject<Car>(stringResponse);
            Assert.Equal(2, car.Id);
        }

        [Fact]
        public async Task GivenNewCar_WhenSave_ThenCarSaved()
        {
            var car = new Car()
            {
                CarNr = "C123",
                Class = CarClass.Medium,
                Type = CarType.Convertible,
                Brand = "BMW"
            };
            var response = await _client.PostAsJsonAsync("car", car);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseCar = JsonConvert.DeserializeObject<Car>(stringResponse);
            Assert.NotNull(responseCar.Id);
        }

        [Fact]
        public async Task GivenUpdatedCar_WhenUpdate_ThenCarUpdated()
        {
            var car = new Car()
            {
                Id = 3,
                CarNr = "C123",
                Class = CarClass.Medium,
                Type = CarType.Convertible,
                Brand = "BMW"
            };
            var httpContent = new StringContent(JsonConvert.SerializeObject(car), 
                Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync("car", httpContent);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseCar = JsonConvert.DeserializeObject<Car>(stringResponse);
            Assert.Equal(3, responseCar.Id);
            Assert.Equal(CarClass.Medium, responseCar.Class);
        }

        [Fact]
        public async Task GivenCarId_WhenDelete_ThenCarIsDeleted()
        {
            var response = await _client.DeleteAsync("car/1");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseCar = JsonConvert.DeserializeObject<Car>(stringResponse);
            Assert.NotNull(responseCar.Id);
        }

        [Fact]
        public async Task GivenListOfCar_WhenSearch_ThenMatchingCarsAreReturned()
        {
            var response = await _client.GetAsync("car/find/vw");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<IEnumerable<Car>>(stringResponse);
            Assert.Equal(4, cars.Count());
        }
    }
}
