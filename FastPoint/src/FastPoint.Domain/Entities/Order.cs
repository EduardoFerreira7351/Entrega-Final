using System;
using System.Collections.Generic;

namespace FastPoint.Domain.Entities
{
    public enum OrderStatus
    {
        Created = 0,
        Confirmed = 1,
        Preparing = 2,
        Ready = 3,
        OutForDelivery = 4,
        Completed = 5,
        Cancelled = 6
    }

    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Created;

        // relações
        public Guid CustomerId { get; set; }
        public User? Customer { get; set; }

        public ICollection<OrderItem>? Items { get; set; }

        // finance
        public bool HasPendingPayment { get; set; } = true;
        public string? DeliveryAddress { get; set; } // null se for balcão
    }
}
