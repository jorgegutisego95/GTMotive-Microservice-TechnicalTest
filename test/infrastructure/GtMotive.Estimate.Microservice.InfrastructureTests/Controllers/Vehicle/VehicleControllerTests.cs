using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Controllers;
using GtMotive.Estimate.Microservice.Api.Dtos;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Vehicle.Controllers
{
    /// <summary>
    /// Infrastructure-level tests for <see cref="VehicleController"/>.
    /// Focuses on request reception and model validation at the host level.
    /// </summary>
    [Collection(TestCollections.TestServer)]
    public class VehicleControllerTests
    {
        private readonly HttpClient _client;

        public VehicleControllerTests(GenericInfrastructureTestServerFixture fixture)
        {
            ArgumentNullException.ThrowIfNull(fixture);
            _client = fixture.Client;
        }

        [Fact]
        public async Task CreateVehicleNullDtoReturnsBadRequest()
        {
            VehicleDto nullDto = null;
            var response = await _client.PostAsJsonAsync("/api/vehicle", nullDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateVehicleInvalidManufactureDateReturnsInvalidOperationException()
        {
            var invalidDto = new VehicleDto
            {
                Model = "TestModel",
                Plate = "ABC123",
                ManufactureDate = DateTime.UtcNow.AddYears(-6)
            };
            var response = await _client.PostAsJsonAsync("/api/vehicle", invalidDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateVehicleValidDtoReturnsOk()
        {
            var validDto = new VehicleDto
            {
                Model = "TestModel",
                Plate = "ABC123",
                ManufactureDate = DateTime.UtcNow.AddYears(-1)
            };
            var response = await _client.PostAsJsonAsync("/api/vehicle", validDto);

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine("BODY: " + body);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var createdVehicle = await response.Content.ReadFromJsonAsync<VehicleDto>();
            Assert.NotNull(createdVehicle);
            Assert.Equal(validDto.Model, createdVehicle.Model);
            Assert.Equal(validDto.Plate, createdVehicle.Plate);
            Assert.Equal(validDto.ManufactureDate, createdVehicle.ManufactureDate);
        }
    }
}
