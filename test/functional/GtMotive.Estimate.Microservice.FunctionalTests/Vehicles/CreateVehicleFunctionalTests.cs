using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Commands;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Vehicles
{
    [Collection(TestCollections.Functional)]
    public class CreateVehicleFunctionalTests(CompositionRootTestFixture fixture) : FunctionalTestBase(fixture)
    {
        [Fact]
        public async Task ShouldCreateVehicleSuccessfullyDataIsValid()
        {
            var command = new CreateVehicleCommand("TestModel", "ABC123", DateTime.UtcNow.AddYears(-1));

            await Fixture.UsingHandlerForRequestResponse<CreateVehicleCommand, VehicleDto>(
            async handler =>
            {
                var result = await handler.Handle(command, CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal("TestModel", result.Model);
                Assert.Equal("ABC123", result.Plate);
            });
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenVehicleIsOlderThanFiveYears()
        {
            var command = new CreateVehicleCommand("TestModel", "ABC123", DateTime.UtcNow.AddYears(-6));
            await Assert.ThrowsAsync<InvalidOperationException>(() => Fixture.UsingHandlerForRequestResponse<CreateVehicleCommand, VehicleDto>(
                    handler => handler.Handle(command, CancellationToken.None)));
        }
    }
}
