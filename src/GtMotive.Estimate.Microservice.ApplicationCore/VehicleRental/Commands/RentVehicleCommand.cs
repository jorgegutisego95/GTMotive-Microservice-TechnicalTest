using System;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Commands
{
    /// <summary>
    /// Command to request renting a vehicle.
    /// Contains the identifier of the vehicle to rent and the user's DNI.
    /// This command is handled by a MediatR handler that will process the rental
    /// and return a <see cref="VehicleRentalDto"/> representing the created rental.
    /// </summary>
    /// <param name="VehicleId">The unique identifier of the vehicle to be rented.</param>
    /// <param name="Dni">The DNI of the user who wants to rent the vehicle.</param>
    /// <param name="RentDate">The Rent Date of the vehicle, if null will be the currentDate.</param>
    public record RentVehicleCommand(Guid VehicleId, string Dni, DateTime? RentDate) : IRequest<VehicleRentalDto>;
}
