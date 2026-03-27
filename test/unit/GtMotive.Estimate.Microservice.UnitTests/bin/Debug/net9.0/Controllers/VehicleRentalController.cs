using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Commands;
using GtMotive.Estimate.Microservice.ApplicationCore.VehicleRental.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/vehicleRental")]
    public class VehicleRentalController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("rent")]
        public async Task<IActionResult> RentVehicle([FromBody] VehicleRentalDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Renting data is required.");
            }

            var result = await _mediator.Send(new RentVehicleCommand(dto.VehicleId, dto.Dni, dto.RentDate));
            return Ok(result);
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnVehicle([FromBody] VehicleRentalDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Return data is required.");
            }

            var result = await _mediator.Send(new ReturnVehicleCommand(dto.VehicleId, dto.Dni, dto.ReturnDate));
            return Ok(result);
        }

        [HttpGet("getAvailables")]
        public async Task<IActionResult> GetVehicleAvailables()
        {
            var vehicles = await _mediator.Send(new GetAvailableVehiclesQuery());
            return Ok(vehicles);
        }
    }
}
