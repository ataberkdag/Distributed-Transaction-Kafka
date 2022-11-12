using Core.Domain.Abstractions.Persistence;

namespace Order.Domain.Abstractions
{
    public interface IOrderRepository : IRepository<Order.Domain.Entities.Order>
    {
    }
}
