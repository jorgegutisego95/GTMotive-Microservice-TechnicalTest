using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Entities
{
    /// <summary>
    /// Represents a rental of a vehicle by a user (DNI).
    /// </summary>
    public class VehicleRental
    {
        /// <summary>
        /// Gets or sets the unique rental identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the vehicle being rented.
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier (DNI) renting the vehicle.
        /// </summary>
        public string Dni { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the vehicle was rented.
        /// </summary>
        public DateTime RentDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the vehicle was returned. Null by default, indicating the vehicle is still rented.
        /// </summary>
        public DateTime? ReturnDate { get; set; }
    }
}
