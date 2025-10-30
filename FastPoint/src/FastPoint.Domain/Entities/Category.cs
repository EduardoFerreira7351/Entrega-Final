using System;
using System.Collections.Generic;

namespace FastPoint.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Product>? Products { get; set; }
    }
}
