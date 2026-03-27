using System;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Commands
{
    /// <summary>
    /// Command to request returning a rented vehicle.
    /// Contains the identifier of the vehicle to return and the user's DNI.
    /// This command is handled by a MediatR handler that will process the return
    /// and return a <see cref="VehicleRentalDto"/> representing the updated rental.
    /// </summary>
    /// <param name="VehicleId">The unique identifier of the vehicle being returned.</param>
    /// <param name="Dni">The DNI of the user who is returning the vehicle.</param>
    /// <param name="ReturnDate">The Return Date of the vehicle, if null will be the currentDate.</param>
    public record ReturnVehicleCommand(Guid VehicleId, string Dni, DateTime? ReturnDate) : IRequest<VehicleRentalDto>;
}
