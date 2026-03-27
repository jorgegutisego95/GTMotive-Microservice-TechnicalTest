using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Repositories
{
    /// <summary>
    /// Repository interface to manage rental vehicles in the fleet.
    /// Defines the operations required by the application core to manage vehicle rentals.
    /// </summary>
    public interface IVehicleRentalRepository
    {
        /// <summary>
        /// Gets all rented vehicles.
        /// </summary>
        /// <returns>A collection of <see cref="Entities.VehicleRental"/> instances.</returns>
        Task<IEnumerable<Entities.VehicleRental>> GetAllRentedVehiclesAsync();

        /// <summary>
        /// Adds a new vehicle rental to the repository.
        /// </summary>
        /// <param name="vehicleRental">The <see cref="Entities.VehicleRental"/> to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddAsync(Entities.VehicleRental vehicleRental);

        /// <summary>
        /// Updates an existing vehicle rental to the repository.
        /// </summary>
        /// <param name="vehicleRental">The <see cref="Entities.VehicleRental"/> to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(Entities.VehicleRental vehicleRental);

        /// <summary>
        /// Determines whether there is an existing active rental for the provided identity.
        /// </summary>
        /// <param name="dni">The identity number (DNI) of the customer to check.</param>
        /// <returns>
        /// A task that resolves to <c>true</c> if there is at least one active rental associated with the given DNI;
        /// otherwise <c>false</c>.
        /// </returns>
        Task<bool> HasAnExistingRental(string dni);

        /// <summary>
        /// Retrieves a specific rental by vehicle identifier and customer DNI.
        /// </summary>
        /// <param name="vehicleId">Unique identifier of the vehicle (<see cref="Guid"/>).</param>
        /// <param name="dni">DNI of the customer associated with the rental.</param>
        /// <returns>
        /// A task that resolves to the <see cref="Entities.VehicleRental"/> if found; otherwise <c>null</c>.
        /// </returns>
        Task<Entities.VehicleRental> GetRentalByDniAndVehicleId(Guid vehicleId, string dni);
    }
}
