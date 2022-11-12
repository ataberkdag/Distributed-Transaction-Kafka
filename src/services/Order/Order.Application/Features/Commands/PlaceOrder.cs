using Core.Application.Common;
using MediatR;
using Order.Domain.Abstractions;
using Order.Domain.Dtos;

namespace Order.Application.Features.Commands
{
    public static class PlaceOrder
    {
        public class Command : IRequest<BaseResult<Response>>
        {
            public Guid UserId { get; set; }
            public List<OrderItemDto> OrderItems { get; set; }

        }

        public class CommandHandler : IRequestHandler<Command, BaseResult<Response>>
        {
            private readonly IOrderUnitOfWork _uow;

            public CommandHandler(IOrderUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<BaseResult<Response>> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = Order.Domain.Entities.Order.CreateOrder(request.UserId, request.OrderItems);

                await _uow.Orders.AddAsync(order);

                await _uow.SaveChangesAsync();

                return BaseResult<Response>.Success(new Response { CorrelationId = Guid.NewGuid() });
            }
        }

        public class Response
        {
            public Guid CorrelationId { get; set; }
        }
    }
}
