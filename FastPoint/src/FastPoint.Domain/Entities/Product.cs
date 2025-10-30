using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastPoint.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        // FK
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        // controle de estoque (unidade de medida e quantidade atual gerenciado por Stock)
        public Stock? Stock { get; set; }
    }
}
