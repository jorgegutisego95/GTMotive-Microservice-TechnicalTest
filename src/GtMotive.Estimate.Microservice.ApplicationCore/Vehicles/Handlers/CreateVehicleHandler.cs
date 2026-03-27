using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Entities;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Commands;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Repositories;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Handlers
{
    /// <summary>
    /// Handles the <see cref="CreateVehicleCommand"/> to create a new vehicle in the fleet.
    /// This handler is responsible for validating the command, creating the Vehicle entity,
    /// and returning a <see cref="VehicleDto"/> with the created vehicle's details.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateVehicleHandler"/> class.
    /// </remarks>
    /// <param name="vehicleRepository">Gets or sets the vehicle repository used to persist vehicles.</param>
    public class CreateVehicleHandler(IVehicleRepository vehicleRepository) : IRequestHandler<CreateVehicleCommand, VehicleDto>
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        /// <summary>
        /// Handles the <see cref="CreateVehicleCommand"/> request.
        /// </summary>
        /// <param name="request">Gets or sets the command containing the vehicle data to create.</param>
        /// <param name="cancellationToken">Gets or sets the cancellation token.</param>
        /// <returns>
        /// A <see cref="VehicleDto"/> containing the information of the newly created vehicle.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the vehicle manufacture date is more than 5 years old.
        /// </exception>
        public async Task<VehicleDto> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            if ((DateTime.Now.Year - request.ManufactureDate.Year) > 5)
            {
                throw new InvalidOperationException("Vehicle cannot be older than 5 years.");
            }

            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Model = request.Model,
                Plate = request.Plate,
                ManufactureDate = request.ManufactureDate
            };

            await _vehicleRepository.AddAsync(vehicle);

            return new VehicleDto
            {
                Model = vehicle.Model,
                Plate = vehicle.Plate,
                ManufactureDate = vehicle.ManufactureDate
            };
        }
    }
}
