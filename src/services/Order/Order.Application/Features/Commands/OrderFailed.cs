using Core.Application.Common;
using MediatR;
using Order.Domain.Abstractions;

namespace Order.Application.Features.Commands
{
    public static class OrderFailed
    {
        public class Command : IRequest<BaseResult>
        {
            public Guid CorrelationId { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, BaseResult>
        {
            private readonly IOrderUnitOfWork _uow;

            public CommandHandler(IOrderUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<BaseResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = (await _uow.Orders.FindByQuery(x => x.CorrelationId == request.CorrelationId)).FirstOrDefault();

                if (order == null)
                    return BaseResult.Fail("9999", "Order Not Found");

                order.FailOrder("Insufficient Stock");

                await _uow.SaveChangesAsync();

                return BaseResult.Success();
            }
        }
    }
}
