namespace Messages.Events
{
    public class StockFailedIE : IntegrationEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }

        public StockFailedIE(Guid correlationId, Guid userId) : base(typeof(StockFailedIE).AssemblyQualifiedName)
        {
            CorrelationId = correlationId;
            UserId = userId;
            Type = typeof(StockFailedIE).AssemblyQualifiedName;
        }
    }
}
