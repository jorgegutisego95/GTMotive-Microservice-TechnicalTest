using System.Collections.Generic;
using GtMotive.Estimate.Microservice.ApplicationCore.Dtos;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Queries
{
    /// <summary>
    /// Query to get all available vehicles in the fleet.
    /// </summary>
    public class GetAvailableVehiclesQuery : IRequest<IEnumerable<VehicleDto>>
    {
    }
}
