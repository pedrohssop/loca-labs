using LocaLabs.Api.Services;
using LocaLabs.Application.Commands.Cars.Rents.RegisterCheckList;
using LocaLabs.Application.Commands.Cars.Rents.Simulate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Api.Controllers
{
    [ApiController]
    [Route("rents")]
    public class RentController : ControllerBase
    {
        [HttpGet("simulate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Simulate(
            [FromServices] IOutputBuilderService output, [FromServices] IMediator dispatcher, [FromBody] RentSimulateCmd cmd, CancellationToken token)
        {
            var result = await dispatcher.Send(cmd, token);
            if (result.IsValid)
                return Ok(output.CreateOutput(result.Data));

            return BadRequest(output.CreateOutput());
        }

        [HttpPut("check")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> CheckList(
            [FromServices] IOutputBuilderService output, [FromServices] IMediator dispatcher, [FromBody] RegisterCheckListCmd cmd, CancellationToken token)
        {
            var result = await dispatcher.Send(cmd, token);
            if (result.IsValid)
                return Ok(output.CreateOutput(result.Data));

            return BadRequest(output.CreateOutput());
        }
    }
}