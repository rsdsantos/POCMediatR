using Communication.API.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Communication.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/emails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmailsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendAsync([FromBody] SendEmailCommand command)
        {
            var commandResult = await _mediator.Send(command);

            if (string.IsNullOrEmpty(commandResult))
                return BadRequest();

            return Ok(commandResult);
        }
    }
}
