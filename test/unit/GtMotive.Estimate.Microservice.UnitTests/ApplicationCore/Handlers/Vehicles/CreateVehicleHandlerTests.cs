using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Entities;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Commands;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Handlers;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Repositories;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.Handlers.Vehicles
{
    public class CreateVehicleHandlerTests
    {
        [Fact]
        public async Task HandleShouldCreateVehicleWhenDataIsValid()
        {
            var repoMock = new Mock<IVehicleRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<Vehicle>()))
                    .Returns(Task.CompletedTask);

            var handler = new CreateVehicleHandler(repoMock.Object);
            var command = new CreateVehicleCommand("TestModel", "ABC123", DateTime.UtcNow.AddYears(-1));
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("TestModel", result.Model);
            Assert.Equal("ABC123", result.Plate);
            repoMock.Verify(r => r.AddAsync(It.IsAny<Vehicle>()), Times.Once);
        }

        [Fact]
        public async Task HandleShouldThrowExceptionWhenVehicleIsOlderThanFiveYears()
        {
            var repoMock = new Mock<IVehicleRepository>();
            var handler = new CreateVehicleHandler(repoMock.Object);
            var command = new CreateVehicleCommand("TestModel", "ABC123", DateTime.UtcNow.AddYears(-6));
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal("Vehicle cannot be older than 5 years.", exception.Message);
            repoMock.Verify(r => r.AddAsync(It.IsAny<Vehicle>()), Times.Never);
        }
    }
}
