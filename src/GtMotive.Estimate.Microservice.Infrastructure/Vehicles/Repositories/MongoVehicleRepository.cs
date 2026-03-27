using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Entities;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Vehicles.Repositories
{
    /// <summary>
    /// In-memory implementation of <see cref="IVehicleRepository"/>.
    /// </summary>
    public class MongoVehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _vehicles;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoVehicleRepository"/> class.
        /// </summary>
        /// <param name="mongoService">The <see cref="MongoService"/> used to access the database.</param>
        public MongoVehicleRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _vehicles = mongoService.Database.GetCollection<Vehicle>("Vehicles");
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            await _vehicles.InsertOneAsync(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _vehicles.Find(_ => true).ToListAsync();
        }

        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            return await _vehicles.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            await _vehicles.ReplaceOneAsync(v => v.Id == vehicle.Id, vehicle);
        }
    }
}
