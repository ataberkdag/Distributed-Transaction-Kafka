using Core.Domain.Abstractions;
using Order.Domain.Dtos;

namespace Order.Domain.Events
{
    public class OrderPlaced : IDomainEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

        public OrderPlaced(Guid correlationId, Guid userId, List<OrderItemDto> orderItems)
        {
            CorrelationId = correlationId;
            UserId = userId;
            OrderItems = orderItems;
        }
    }
}
