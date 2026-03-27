using System;
using System.Text.Json.Serialization;

namespace GtMotive.Estimate.Microservice.Api.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) to rent or return a vehicle.
    /// Contains both the vehicle identifier and the user's DNI.
    /// </summary>
    public class VehicleRentalDto
    {
        /// <summary>
        /// Gets or sets the unique rental identifier.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the vehicle being rented.
        /// </summary>
        [JsonRequired]
        public Guid VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier (DNI) renting the vehicle.
        /// </summary>
        [JsonRequired]
        public string Dni { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the vehicle was rented.
        /// </summary>
        public DateTime? RentDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the vehicle was returned. Null by default, indicating the vehicle is still rented.
        /// </summary>
        public DateTime? ReturnDate { get; set; }
    }
}
