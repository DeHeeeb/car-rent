using System;
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
    public class ReservationIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ReservationIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            var options = factory.Services.GetRequiredService<DbContextOptions<ProjectContext>>();
            factory.SetupDb(options);
        }

        [Fact]
        public async Task GivenListOfReservations_WhenGetAll_ThenListIsReturned()
        {
            var response = await _client.GetAsync("reservation");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var reservations = JsonConvert.DeserializeObject<IEnumerable<Reservation>>(stringResponse);
            Assert.Equal(6, reservations.Count());
        }

        [Fact]
        public async Task GivenListOfReservations_WhenGetId2_ThenReservationWithId2Returned()
        {
            var response = await _client.GetAsync("reservation/2");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var reservation = JsonConvert.DeserializeObject<Reservation>(stringResponse);
            Assert.Equal(2, reservation.Id);
        }

        [Fact]
        public async Task GivenNewReservation_WhenSave_ThenReservationSaved()
        {
            var reservation = new Reservation()
            {
                ReservationNr = "R111",
                CustomerId = 2,
                CarClass = CarClass.Medium,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 02)
            };
            var response = await _client.PostAsJsonAsync("reservation", reservation);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseReservation = JsonConvert.DeserializeObject<Reservation>(stringResponse);
            Assert.NotNull(responseReservation.Id);
        }

        [Fact]
        public async Task GivenUpdatedReservation_WhenUpdate_ThenReservationUpdated()
        {
            var reservation = new Reservation()
            {
                Id = 5,
                ReservationNr = "R111",
                CustomerId = 2,
                CarClass = CarClass.Medium,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 02)
            };
            var httpContent = new StringContent(JsonConvert.SerializeObject(reservation), 
                Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync("reservation", httpContent);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseReservation = JsonConvert.DeserializeObject<Reservation>(stringResponse);
            Assert.Equal(5, responseReservation.Id);
            Assert.Equal(2, responseReservation.CustomerId);
        }

        [Fact]
        public async Task GivenReservationId_WhenDelete_ThenReservationIsDeleted()
        {
            var response = await _client.DeleteAsync("reservation/1");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseReservation = JsonConvert.DeserializeObject<Reservation>(stringResponse);
            Assert.NotNull(responseReservation.Id);
        }

        [Fact]
        public async Task GivenValidReservation_WheCalculate_ThenCorrectPriceIsReturned()
        {
            var reservation = new Reservation()
            {
                CarClass = CarClass.High,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 08)
            };
            var response = await _client.PostAsJsonAsync("reservation/calculate", reservation);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(1200, double.Parse(stringResponse));
        }

        [Fact]
        public async Task GivenReservationWithIsContractFalse_WhenConvert_ThenContractIsReturned()
        {
            var response = await _client.PatchAsync("reservation/5/contract", null);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var responseReservation = JsonConvert.DeserializeObject<Reservation>(stringResponse);
            Assert.Equal(5, responseReservation.Id);
            Assert.True(responseReservation.IsContract);
            Assert.Equal(720, responseReservation.Total);
        }

        [Fact]
        public async Task GivenListOfReservationsWithIsContract_WhenGetAllContracts_ThenListIsReturned()
        {
            var response = await _client.GetAsync("reservation/contracts");

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var reservations = JsonConvert.DeserializeObject<IEnumerable<Reservation>>(stringResponse);
            Assert.Equal(3, reservations.Count());
        }

    }
}
