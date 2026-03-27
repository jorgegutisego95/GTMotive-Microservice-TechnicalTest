using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a vehicle.
    /// Used to expose vehicle information from the ApplicationCore layer
    /// to the API or microservice clients.
    /// </summary>
    public class VehicleDto
    {
        /// <summary>
        /// Gets or sets the Vehicle Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Vehicle Model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the Vehicle Plate.
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// Gets or sets the Vehicle Manufacture Date.
        /// Only vehicles with a maximum age of 5 years are allowed.
        /// </summary>
        public DateTime ManufactureDate { get; set; }
    }
}
