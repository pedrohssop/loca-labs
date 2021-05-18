using LocaLabs.Api.Services;
using LocaLabs.Application.Commands.Cars.CreateBrand;
using LocaLabs.Application.Commands.Cars.CreateCar;
using LocaLabs.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Api.Controllers
{
    [ApiController]
    [Route("cars")]
    public class CarController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize]
        public async Task<IActionResult> GetAll(
            [FromServices] IOutputBuilderService output, [FromServices] ICarRepository carRepo, CancellationToken token)
        {
            var result = await carRepo.All(token);
            return Ok(output.CreateOutput(result));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "operator")]
        public async Task<IActionResult> RegisterNewCar(
            [FromServices] IOutputBuilderService output, [FromServices] IMediator dispatcher, [FromBody] CreateCarCmd cmd, CancellationToken token)
        {
            var result = await dispatcher.Send(cmd, token);
            if (result.IsValid)
                return Ok(output.CreateOutput(result.Data));

            return BadRequest(output.CreateOutput());
        }

        [HttpPost("brands")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "operator")]
        public async Task<IActionResult> RegisterNewBrand(
            [FromServices] IOutputBuilderService output, [FromServices] IMediator dispatcher, [FromBody] CreateBrandCmd cmd, CancellationToken token)
        {
            var result = await dispatcher.Send(cmd, token);
            if (result.IsValid)
                return Ok(output.CreateOutput(result.Data));

            return BadRequest(output.CreateOutput());
        }
    }
}