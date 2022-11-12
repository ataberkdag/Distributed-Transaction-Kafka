namespace Messages.Events
{
    public class StockFailedIE : IntegrationEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }

        public StockFailedIE(Guid correlationId, Guid userId)
        {
            CorrelationId = correlationId;
            UserId = userId;
        }
    }
}
