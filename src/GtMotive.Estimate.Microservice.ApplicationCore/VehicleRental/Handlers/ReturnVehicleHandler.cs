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
    /// Handler responsible for processing <see cref="ReturnVehicleCommand"/>.
    /// Updates an existing rental record in the <see cref="IVehicleRentalRepository"/>
    /// and returns a <see cref="VehicleRentalDto"/> representing the rented vehicle.
    /// </summary>
    public class ReturnVehicleHandler(IVehicleRentalRepository vehicleRentalRepository) : IRequestHandler<ReturnVehicleCommand, VehicleRentalDto>
    {
        private readonly IVehicleRentalRepository _vehicleRentalRepository = vehicleRentalRepository;

        /// <summary>
        /// Handles the <see cref="ReturnVehicleCommand"/> request.
        /// Updathes the existing rental with the provided VehicleId and DNI, and sets the ReturnDate,
        /// stores it in the repository, and returns the corresponding <see cref="VehicleRentalDto"/>.
        /// </summary>
        /// <param name="request">The command containing VehicleId and user DNI.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A <see cref="VehicleRentalDto"/> representing the rental updated.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="request"/> is null.</exception>
        public async Task<VehicleRentalDto> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
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

            if (!await _vehicleRentalRepository.HasAnExistingRental(request.Dni))
            {
                throw new InvalidOperationException($"User with DNI {request.Dni} already doesnt has an active rental.");
            }

            var vehicleRental = await _vehicleRentalRepository.GetRentalByDniAndVehicleId(request.VehicleId, request.Dni);

            vehicleRental.ReturnDate = request.ReturnDate ?? DateTime.UtcNow;

            await _vehicleRentalRepository.UpdateAsync(vehicleRental);

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
