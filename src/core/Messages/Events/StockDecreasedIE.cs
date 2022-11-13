namespace Messages.Events
{
    public class StockDecreasedIE : IntegrationEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }

        public StockDecreasedIE(Guid correlationId, Guid userId) : base(typeof(StockDecreasedIE).AssemblyQualifiedName)
        {
            CorrelationId = correlationId;
            UserId = userId;
            Type = typeof(StockDecreasedIE).AssemblyQualifiedName;
        }
    }
}
