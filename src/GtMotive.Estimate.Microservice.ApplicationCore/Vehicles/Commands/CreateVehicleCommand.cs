using System;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Commands
{
    /// <summary>
    /// Command to create a new vehicle in the fleet.
    /// This command is handled by MediatR and returns a <see cref="VehicleDto"/> after creation.
    /// </summary>
    /// <param name="Model">Gets or sets the model of the vehicle. Example: "Toyota Corolla".</param>
    /// <param name="Plate">Gets or sets the license plate of the vehicle. Example: "ABC-1234".</param>
    /// <param name="ManufactureDate">Gets or sets the manufacture date of the vehicle. Only vehicles up to 5 years old are allowed.</param>
    public record CreateVehicleCommand(string Model, string Plate, DateTime ManufactureDate) : IRequest<VehicleDto>;
}
