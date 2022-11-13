namespace Messages.Events
{
    public class OrderPlacedIE : IntegrationEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderItemIEDto> OrderItems { get; set; }

        public OrderPlacedIE(Guid correlationId, Guid userId, List<OrderItemIEDto> orderItems) : base(typeof(OrderPlacedIE).AssemblyQualifiedName)
        {
            CorrelationId = correlationId;
            UserId = userId;
            OrderItems = orderItems;
            Type = typeof(OrderPlacedIE).AssemblyQualifiedName;
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
