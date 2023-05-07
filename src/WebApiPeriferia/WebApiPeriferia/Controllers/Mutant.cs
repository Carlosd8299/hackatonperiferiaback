using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using WebApiPeriferia.Command;
using WebApiPeriferia.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiPeriferia.Controllers
{
    [Route("")]
    [ApiController]
    public class Mutant : ControllerBase
    {
        private readonly IMediator _mediator;

        public Mutant(
           IMediator mediator
        )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // POST api/<Mutant>
        [HttpPost("mutant")]
        public async Task<IActionResult> Post([FromBody] PostMutantDnaCommand dnaCommand)
        {
            if (await _mediator.Send(dnaCommand))
            {
                return StatusCode(StatusCodes.Status200OK, null);
            }
            return StatusCode(StatusCodes.Status403Forbidden, null);
        }

        // POST api/<Mutant>
        [HttpGet("stats")]
        public async Task<IActionResult> Stats([FromQuery] GetStatsQuery query)
        {

            return StatusCode(StatusCodes.Status200OK, await _mediator.Send(query));
        
        }
    }
}
