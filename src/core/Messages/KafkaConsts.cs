namespace Messages
{
    public static class KafkaConsts
    {
        public const string OrderPlacedTopicName = "order_created";
        public const string StockDecreasedTopicName = "stock_decreased";
        public const string StockFailedTopicName = "stock_failed";
    }
}
