using FastPoint.Domain.Entities;

namespace FastPoint.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }

    public interface IProductRepository : IRepository<Product> { }

    public interface ICategoryRepository : IRepository<Category> { }

    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetOrderWithItemsAsync(Guid id);
    }

    public interface IStockRepository : IRepository<Stock> { }
}
