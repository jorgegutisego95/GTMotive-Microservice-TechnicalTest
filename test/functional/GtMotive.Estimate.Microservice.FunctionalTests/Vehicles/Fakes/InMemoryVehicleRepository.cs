using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Repositories;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Vehicle.Fakes
{
    /// <summary>
    /// In-memory implementation of IVehicleRepository for integration tests.
    /// </summary>
    public class InMemoryVehicleRepository : IVehicleRepository
    {
        private readonly List<ApplicationCore.Entities.Vehicle> _vehicles = new();

        public Task AddAsync(ApplicationCore.Entities.Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            _vehicles.Add(vehicle);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<ApplicationCore.Entities.Vehicle>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<ApplicationCore.Entities.Vehicle>>([.. _vehicles]);
        }

        public Task<ApplicationCore.Entities.Vehicle> GetByIdAsync(Guid id)
        {
            var vehicle = _vehicles.FirstOrDefault(v => v.Id == id);
            return Task.FromResult(vehicle);
        }

        public Task UpdateAsync(ApplicationCore.Entities.Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var index = _vehicles.FindIndex(v => v.Id == vehicle.Id);
            if (index >= 0)
            {
                _vehicles[index] = vehicle;
            }

            return Task.CompletedTask;
        }
    }
}
