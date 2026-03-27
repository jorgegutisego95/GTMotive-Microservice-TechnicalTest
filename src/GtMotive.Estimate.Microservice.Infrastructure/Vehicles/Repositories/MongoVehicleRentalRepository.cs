using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Entities;
using GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Vehicles.Repositories
{
    /// <summary>
    /// In-memory implementation of <see cref="IVehicleRentalRepository"/>.
    /// </summary>
    public class MongoVehicleRentalRepository : IVehicleRentalRepository
    {
        private readonly IMongoCollection<VehicleRental> _vehicleRentals;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoVehicleRentalRepository"/> class.
        /// </summary>
        /// <param name="mongoService">The <see cref="MongoService"/> used to access the database.</param>
        public MongoVehicleRentalRepository(MongoService mongoService)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            _vehicleRentals = mongoService.Database.GetCollection<VehicleRental>("VehicleRentals");
        }

        public async Task<IEnumerable<VehicleRental>> GetAllRentedVehiclesAsync()
        {
            return await _vehicleRentals.Find(v => v.ReturnDate == null).ToListAsync();
        }

        public async Task AddAsync(VehicleRental vehicleRental)
        {
            ArgumentNullException.ThrowIfNull(vehicleRental);
            await _vehicleRentals.InsertOneAsync(vehicleRental);
        }

        public async Task UpdateAsync(VehicleRental vehicleRental)
        {
            ArgumentNullException.ThrowIfNull(vehicleRental);

            var filter = Builders<VehicleRental>.Filter.Eq(v => v.Id, vehicleRental.Id);
            var options = new ReplaceOptions { IsUpsert = false };
            var result = await _vehicleRentals.ReplaceOneAsync(filter, vehicleRental, options);

            if (result.MatchedCount == 0)
            {
                throw new InvalidOperationException($"VehicleRental with Id {vehicleRental.Id} was not found.");
            }
        }

        public async Task<bool> HasAnExistingRental(string dni)
        {
            ArgumentNullException.ThrowIfNull(dni);
            return await _vehicleRentals.Find(vr => vr.Dni == dni).AnyAsync();
        }

        public async Task<VehicleRental> GetRentalByDniAndVehicleId(Guid vehicleId, string dni)
        {
            ArgumentNullException.ThrowIfNull(dni);
            return await _vehicleRentals.Find(vr => vr.Dni == dni && vr.VehicleId == vehicleId).FirstOrDefaultAsync();
        }
    }
}
