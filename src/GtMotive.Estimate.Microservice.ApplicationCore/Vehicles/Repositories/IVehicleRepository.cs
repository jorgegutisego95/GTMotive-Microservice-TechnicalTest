using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Repositories
{
    /// <summary>
    /// Repository interface to manage vehicles in the fleet.
    /// Defines the operations required by the application core to manage vehicles.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Gets a vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>The <see cref="Vehicle"/> if found; otherwise, null.</returns>
        Task<Vehicle> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds a new vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">The <see cref="Vehicle"/> to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// Updates an existing vehicle in the repository.
        /// </summary>
        /// <param name="vehicle">The <see cref="Vehicle"/> to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(Vehicle vehicle);

        /// <summary>
        /// Gets all vehicles.
        /// </summary>
        /// <returns>A collection of <see cref="Vehicle"/> instances.</returns>
        Task<IEnumerable<Vehicle>> GetAllAsync();
    }
}
