using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using car_rent_backend.domain;
using car_rent_backend.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace car_rent_backend.Tests.integration
{
    public class CustomerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CustomerIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            var options = factory.Services.GetRequiredService<DbContextOptions<ProjectContext>>();
            factory.SetupDb(options);
        }

        [Fact]
        public async Task GivenListOfCustomers_WhenGetAll_ThenListIsReturned()
        {
            var response = await _client.GetAsync("customer");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(stringResponse);
            Assert.Equal(5, customers.Count());
        }

        [Fact]
        public async Task GivenListOfCustomers_WhenGetId2_ThenCustomerWithId2Returned()
        {
            var response = await _client.GetAsync("customer/2");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(stringResponse);
            Assert.Equal(2, customer.Id);
        }

        [Fact]
        public async Task GivenNewCustomer_WhenSave_ThenCustomerSaved()
        {
            var customer = new Customer()
            {
                CustomerNr = "ABC",
                FirstName = "Lukas",
                LastName = "Heeb",
                Street = "Buchenstrasse 11",
                Zip = 9000,
                City = "St. Gallen"
            };
            var response = await _client.PostAsJsonAsync("customer", customer);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseCustomer = JsonConvert.DeserializeObject<Customer>(stringResponse);
            Assert.NotNull(responseCustomer.Id);
        }

        [Fact]
        public async Task GivenUpdatedCustomer_WhenUpdate_ThenCustomerUpdated()
        {
            var customer = new Customer()
            {
                Id = 3,
                CustomerNr = "ABC",
                FirstName = "Lukas",
                LastName = "Heeb",
                Street = "Buchenstrasse 11",
                Zip = 9000,
                City = "St. Gallen"
            };
            var httpContent = new StringContent(JsonConvert.SerializeObject(customer), 
                Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync("customer", httpContent);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseCustomer = JsonConvert.DeserializeObject<Customer>(stringResponse);
            Assert.Equal(3, responseCustomer.Id);
            Assert.Equal("Lukas", responseCustomer.FirstName);
        }

        [Fact]
        public async Task GivenCustomerId_WhenDelete_ThenCustomerIsDeleted()
        {
            var response = await _client.DeleteAsync("customer/1");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseCustomer = JsonConvert.DeserializeObject<Customer>(stringResponse);
            Assert.NotNull(responseCustomer.Id);
        }

        [Fact]
        public async Task GivenListOfCustomers_WhenSearch_ThenMatchingCustomersAreReturned()
        {
            var response = await _client.GetAsync("customer/find/c4");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(stringResponse);
            Assert.Equal(2, customers.Count());
        }
    }
}
