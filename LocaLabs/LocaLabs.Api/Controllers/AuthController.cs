using LocaLabs.Api.Services;
using LocaLabs.Application.Commands.Users.CreateOperator;
using LocaLabs.Application.Commands.Users.LogIn;
using LocaLabs.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> Get(
            [FromServices] IOutputBuilderService output,
            [FromServices] IAuthService auth,
            [FromServices] IMediator dispatcher,
            [FromBody] LogInCmd logIn,
            CancellationToken cancelToken)
        {
            var result = await dispatcher.Send(logIn, cancelToken);

            if (result.Data.User.IsNone)
                return NotFound();

            var token = auth.GenerateToken((User)result.Data.User);
            if (result.Data.Client.IsSome)
                return Ok(output.CreateOutput(new
                {
                    token,
                    client = (Client)result.Data.Client
                }));

            return Ok(output.CreateOutput(new
            {
                token
            }));
        }

        [AllowAnonymous]
        [HttpPost("operators/signup")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> OperatorSignUp(
            [FromServices] IOutputBuilderService output, [FromServices] IMediator dispatcher, [FromBody] CreateOpUserCmd cmd, CancellationToken token)
        {
            var result = await dispatcher.Send(cmd, token);
            if (result.IsValid)
                return Ok(output.CreateOutput(new
                {
                    Id = result.Data.Id,
                    Name = result.Data.Login,
                    CreatedAt = result.Data.CreatedAt
                }));

            return BadRequest(output.CreateOutput());
        }
    }
}