using System;
using System.Collections.Generic;

namespace FastPoint.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!; // hash armazenado pela infra
        public UserRole Role { get; set; } = UserRole.Client;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // relações
        public ICollection<Order>? Orders { get; set; }
    }
}
