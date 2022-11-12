using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Commands;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrder.Command command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
