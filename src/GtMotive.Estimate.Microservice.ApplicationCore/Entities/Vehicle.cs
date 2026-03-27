using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Entities
{
    /// <summary>
    /// Represents a vehicle in the fleet.
    /// This entity is used internally in the ApplicationCore layer to manage vehicle data.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets the unique identifier of the vehicle.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the vehicle model.
        /// Example: "Toyota Corolla".
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the vehicle license plate.
        /// Example: "ABC-1234".
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// Gets or sets the manufacture date of the vehicle.
        /// Only vehicles up to 5 years old are allowed in the fleet.
        /// </summary>
        public DateTime ManufactureDate { get; set; }
    }
}
