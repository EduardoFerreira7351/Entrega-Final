using System;

namespace FastPoint.Domain.Entities
{
    public class Stock
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; } = 0;
        public int ReorderThreshold { get; set; } = 5; // exemplo
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
