using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Queries;
using GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Repositories;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Repositories;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Handlers
{
    /// <summary>
    /// Handler responsible for processing the <see cref="GetAvailableVehiclesQuery"/>.
    /// Retrieves all vehicles that are currently not rented.
    /// </summary>
    public class GetAvailableVehiclesHandler(IVehicleRepository vehicleRepository, IVehicleRentalRepository rentingRepository) : IRequestHandler<GetAvailableVehiclesQuery, IEnumerable<VehicleDto>>
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IVehicleRentalRepository _rentingRepository = rentingRepository;

        /// <summary>
        /// Handles the <see cref="GetAvailableVehiclesQuery"/> request and returns
        /// a list of <see cref="VehicleDto"/> representing vehicles available for renting.
        /// </summary>
        /// <param name="request">The query request. No additional parameters in this case.</param>
        /// <param name="cancellationToken">Token to cancel the asynchronous operation if needed.</param>
        /// <returns>A collection of <see cref="VehicleDto"/> representing available vehicles.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="request"/> is null.</exception>
        public async Task<IEnumerable<VehicleDto>> Handle(GetAvailableVehiclesQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var allVehicles = await _vehicleRepository.GetAllAsync();
            var activeRentals = await _rentingRepository.GetAllRentedVehiclesAsync();
            var availableVehicles = allVehicles
                .Where(v => !activeRentals.Any(r => r.VehicleId == v.Id))
                .Select(v => new VehicleDto
                {
                    Id = v.Id,
                    Model = v.Model,
                    Plate = v.Plate,
                    ManufactureDate = v.ManufactureDate,
                });

            return availableVehicles;
        }
    }
}
