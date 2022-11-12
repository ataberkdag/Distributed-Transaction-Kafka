namespace Messages.Events
{
    public class StockDecreasedIE : IntegrationEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }

        public StockDecreasedIE(Guid correlationId, Guid userId)
        {
            CorrelationId = correlationId;
            UserId = userId;
        }
    }
}
