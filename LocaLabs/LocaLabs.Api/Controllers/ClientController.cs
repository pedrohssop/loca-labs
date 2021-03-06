using LocaLabs.Api.Services;
using LocaLabs.Application.Commands.Cars.Rents.Schedule;
using LocaLabs.Application.Commands.Clients.CreateClient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Api.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "operator")]
        public async Task<IActionResult> RegisterNewClient(
            [FromServices] IOutputBuilderService output, [FromServices] IMediator dispatcher, [FromBody] CreateClientCmd cmd, CancellationToken token)
        {
            var result = await dispatcher.Send(cmd, token);
            if (result.IsValid)
                return Ok(output.CreateOutput(result.Data));

            return BadRequest(output.CreateOutput());
        }

        [HttpPost("rents/schedule")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> SheduleRent(
            [FromServices] IOutputBuilderService output, [FromServices] IMediator dispatcher, [FromBody] ScheduleRentCmd cmd, CancellationToken token)
        {
            cmd.AssignClient(Guid.Parse(User.Identity.Name));

            var result = await dispatcher.Send(cmd, token);
            if (result.IsValid)
                return Ok(output.CreateOutput(result.Data));

            return BadRequest(output.CreateOutput());
        }
    }
}
