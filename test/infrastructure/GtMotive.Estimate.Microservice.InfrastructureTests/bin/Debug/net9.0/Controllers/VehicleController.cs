using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Dtos;
using GtMotive.Estimate.Microservice.ApplicationCore.Vehicles.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/vehicle")]
    public class VehicleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehicleDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Vehicle data is required.");
                }

                var vehicle = await _mediator.Send(new CreateVehicleCommand(dto.Model, dto.Plate, dto.ManufactureDate));
                return Ok(vehicle);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
