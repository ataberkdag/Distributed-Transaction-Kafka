using Core.Domain.Base;

namespace Order.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public Guid ItemId { get; private set; }
        public int Quantity { get; private set; }

        public long OrderId { get; private set; }
        public Order Order { get; private set; }

        protected OrderItem()
        {

        }

        private OrderItem(Guid itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }

        public static OrderItem CreateOrderItem(Guid itemId, int quantity)
        {
            return new OrderItem(itemId, quantity);
        }
    }
}
