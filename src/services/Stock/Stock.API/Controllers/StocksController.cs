using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stock.Application.Features.Commands;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StocksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddStock")]
        public async Task<IActionResult> AddStock([FromBody] AddStock.Command command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
