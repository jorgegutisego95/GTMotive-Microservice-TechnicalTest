using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Commands;
using GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Repositories;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Handlers
{
    /// <summary>
    /// Handler responsible for processing <see cref="RentVehicleCommand"/>.
    /// Creates a new rental record in the <see cref="IVehicleRentalRepository"/>
    /// and returns a <see cref="VehicleRentalDto"/> representing the rented vehicle.
    /// </summary>
    public class RentVehicleHandler(IVehicleRentalRepository vehicleRentalRepository) : IRequestHandler<RentVehicleCommand, VehicleRentalDto>
    {
        private readonly IVehicleRentalRepository _vehicleRentalRepository = vehicleRentalRepository;

        /// <summary>
        /// Handles the <see cref="RentVehicleCommand"/> request.
        /// Creates a new rental with the provided VehicleId and DNI, sets the RentDate,
        /// stores it in the repository, and returns the corresponding <see cref="VehicleRentalDto"/>.
        /// </summary>
        /// <param name="request">The command containing VehicleId and user DNI.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A <see cref="VehicleRentalDto"/> representing the newly created rental.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="request"/> is null.</exception>
        public async Task<VehicleRentalDto> Handle(RentVehicleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (request.VehicleId == Guid.Empty || string.IsNullOrWhiteSpace(request.Dni))
            {
                throw new ArgumentException("VehicleId cannot be empty.", nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Dni))
            {
                throw new ArgumentException("DNI cannot be empty.", nameof(request));
            }

            if (await _vehicleRentalRepository.HasAnExistingRental(request.Dni))
            {
                throw new InvalidOperationException($"User with DNI {request.Dni} already has an active rental.");
            }

            var vehicleRental = new Entities.VehicleRental
            {
                Id = Guid.NewGuid(),
                VehicleId = request.VehicleId,
                Dni = request.Dni,
                RentDate = request.RentDate ?? DateTime.UtcNow
            };

            await _vehicleRentalRepository.AddAsync(vehicleRental);

            return new VehicleRentalDto
            {
                Id = vehicleRental.Id,
                VehicleId = vehicleRental.VehicleId,
                Dni = vehicleRental.Dni,
                RentDate = vehicleRental.RentDate,
                ReturnDate = vehicleRental.ReturnDate
            };
        }
    }
}
