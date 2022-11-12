using Core.Application.Common;
using MediatR;
using Stock.Domain.Abstractions;

namespace Stock.Application.Features.Commands
{
    public static class AddStock
    {
        public class Command : IRequest<BaseResult>
        {
            public Guid ItemId { get; set; }
            public int Quantity { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command,BaseResult>
        {
            private readonly IStockUnitOfWork _uow;

            public CommandHandler(IStockUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task<BaseResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var stock = Stock.Domain.Entities.Stock.Create(request.ItemId, request.Quantity);

                await _uow.Stocks.AddAsync(stock);

                await _uow.SaveChangesAsync();

                return BaseResult.Success();
            }
        }
    }
}
