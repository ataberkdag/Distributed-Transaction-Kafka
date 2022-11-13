namespace Messages
{
    public abstract class IntegrationEvent
    {
        public string Type { get; set; }

        public IntegrationEvent(string type)
        {
            Type = type;
        }
    }
}
