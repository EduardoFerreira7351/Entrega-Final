using Microsoft.EntityFrameworkCore;
using FastPoint.Domain.Entities;
using FastPoint.Infrastructure.Configurations;

namespace FastPoint.Infrastructure.Context
{
    public class FastPointDbContext : DbContext
    {
        public FastPointDbContext(DbContextOptions<FastPointDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // aplicar todas configurações
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());

            // Seed básico (roles/usuário/alguns produtos) - cuidado: senha deve ser hashed na infra real
            var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var managerId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            modelBuilder.Entity<User>().HasData(
                new User {
                    Id = adminId,
                    Email = "admin@fastpoint.local",
                    FullName = "Administrador FastPoint",
                    PasswordHash = "PlaceholderHash_Admin", // substitua por hash real em produção
                    Role = UserRole.Admin,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User {
                    Id = managerId,
                    Email = "manager@fastpoint.local",
                    FullName = "Gerente FastPoint",
                    PasswordHash = "PlaceholderHash_Manager",
                    Role = UserRole.Manager,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed categorias e produtos
            var catBurgers = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var prodBurger = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = catBurgers, Name = "Burgers", Description = "Hambúrgueres clássicos", IsActive = true }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = prodBurger, Name = "Classic Burger", Description = "Hambúrguer clássico com queijo", Price = 19.90m, IsActive = true, CategoryId = catBurgers }
            );

            modelBuilder.Entity<Stock>().HasData(
                new Stock { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), ProductId = prodBurger, Quantity = 50, ReorderThreshold = 5, LastUpdated = DateTime.UtcNow }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
