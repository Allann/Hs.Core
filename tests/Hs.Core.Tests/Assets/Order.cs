using Hs.Core.Concepts;
using Hs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hs.Core.Tests.Assets
{
    public class Order : Entity<Guid>
    {
        private readonly List<OrderItem> _orderItems = new();

        public Order()
        {
            Id = Guid.NewGuid();
        }

        public IEnumerable<OrderItem> Items => _orderItems;

        public OrderItem AddItem(string code, int qty)
        {
            var item = _orderItems.Find(o => o.Code == code); 
            if(item is null)
            {
                item = OrderItem.AddToOrder(this, code, qty);
                _orderItems.Add(item);
            }
            else
            {
                item.AlterQty(qty);
            }
            return item;
        }
    }

    public class OrderItem : Entity<OrderItemKey>
    {

        private OrderItem(Order order, string code, int qty)
        {
            Id = new OrderItemKey { OrderId = order.Id, OrderItemId = order.Items.Count() + 1 };
            Code = code;
            Qty = qty;
        }

        public OrderItem(Guid id, int itemId, string code, int qty)
        {
            Id = new OrderItemKey { OrderId = id, OrderItemId = itemId };
            Code = code;
            Qty = qty;
        }

        public string Code { get; private set; }

        public int Qty { get; private set; }

        public void AlterQty(int count)
        {
            Qty += count;
        }

        public static OrderItem AddToOrder(Order order, string code, int qty)
        {
            return new OrderItem(order, code, qty);
        }
    }

    public class OrderItemKey:IEquatable<OrderItemKey>
    {
        public Guid OrderId { get; set; }

        public int OrderItemId { get; set; }

        public bool Equals(OrderItemKey other) => 
            OrderId == other.OrderId && OrderItemId == other.OrderItemId;

        public override bool Equals(object obj) => 
            obj is OrderItemKey key && Equals(key);

        public override int GetHashCode() => 
            HashCodeHelper.Generate(OrderId, OrderItemId);
    }
}
