using Core.Domain.Abstractions;
using Order.Domain.Dtos;

namespace Order.Domain.Events
{
    public class OrderCreated : IDomainEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

        public OrderCreated(Guid correlationId, Guid userId, List<OrderItemDto> orderItems)
        {
            CorrelationId = correlationId;
            UserId = userId;
            OrderItems = orderItems;
        }
    }
}
