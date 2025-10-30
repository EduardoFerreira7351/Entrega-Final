using FastPoint.Application.Interfaces;
using FastPoint.Domain.Entities;
using FastPoint.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FastPoint.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FastPointDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }

    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(FastPointDbContext context) : base(context) { }
    }

    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(FastPointDbContext context) : base(context) { }
    }

    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(FastPointDbContext context) : base(context) { }

        public async Task<Order?> GetOrderWithItemsAsync(Guid id)
        {
            return await _dbSet.Include(o => o.Items)
                               .ThenInclude(i => i.Product)
                               .FirstOrDefaultAsync(o => o.Id == id);
        }
    }

    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository(FastPointDbContext context) : base(context) { }
    }
}
