namespace Messages.Events
{
    public class OrderCreatedIE : IntegrationEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderItemIEDto> OrderItems { get; set; }

        public OrderCreatedIE(Guid correlationId, Guid userId, List<OrderItemIEDto> orderItems)
        {
            CorrelationId = correlationId;
            UserId = userId;
            OrderItems = orderItems;
        }
    }

    public class OrderItemIEDto
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }

        public OrderItemIEDto(Guid itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
