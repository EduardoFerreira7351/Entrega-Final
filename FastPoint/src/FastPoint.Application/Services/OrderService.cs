using FastPoint.Application.Interfaces;
using FastPoint.Domain.Entities;

namespace FastPoint.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IUserRepository _userRepo;
        private readonly IProductRepository _productRepo;
        private readonly IStockRepository _stockRepo;

        public OrderService(IOrderRepository orderRepo, IUserRepository userRepo, IProductRepository productRepo, IStockRepository stockRepo)
        {
            _orderRepo = orderRepo;
            _userRepo = userRepo;
            _productRepo = productRepo;
            _stockRepo = stockRepo;
        }

        public async Task<Order> CreateOrderAsync(Guid userId, List<(Guid productId, int qty)> items)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new Exception("Usuário não encontrado.");

            if (user.Role == UserRole.Client && user.IsActive == false)
                throw new Exception("Cliente inativo.");

            var order = new Order
            {
                CustomerId = userId,
                Items = new List<OrderItem>(),
                Status = OrderStatus.Created,
                CreatedAt = DateTime.UtcNow
            };

            decimal total = 0;

            foreach (var (productId, qty) in items)
            {
                var product = await _productRepo.GetByIdAsync(productId)
                    ?? throw new Exception($"Produto {productId} não encontrado.");

                if (!product.IsActive)
                    throw new Exception($"Produto {product.Name} está inativo.");

                var stock = await _stockRepo.FindAsync(s => s.ProductId == productId);
                var stockItem = stock.FirstOrDefault() ?? throw new Exception($"Sem estoque para {product.Name}.");

                if (stockItem.Quantity < qty)
                    throw new Exception($"Estoque insuficiente para {product.Name}.");

                // reduz estoque
                stockItem.Quantity -= qty;
                _stockRepo.Update(stockItem);

                var item = new OrderItem
                {
                    ProductId = productId,
                    Quantity = qty,
                    UnitPrice = product.Price
                };
                order.Items.Add(item);
                total += item.TotalPrice;
            }

            order.Total = total;
            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangesAsync();
            await _stockRepo.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> GetOrderAsync(Guid id) => await _orderRepo.GetOrderWithItemsAsync(id);

        public async Task<IEnumerable<Order>> GetAllAsync() => await _orderRepo.GetAllAsync();
    }
}
