namespace Core.Domain.Base
{
    public class OutboxMessage : EntityBase
    {
        public string Type { get; private set; }

        public string Data { get; private set; }
        public string TopicName { get; private set; }

        protected OutboxMessage()
        {

        }

        private OutboxMessage(string type, string data, string topicName)
        {
            this.Type = type;
            this.Data = data;
            this.TopicName = topicName;
        }

        public static OutboxMessage Create(string type, string data, string topicName)
        {
            return new OutboxMessage(type, data, topicName);
        }
    }
}
