using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastPoint.Application.Interfaces;
using FastPoint.Application.Services;
using FastPoint.Domain.Entities;
using Moq;
using Xunit;

namespace FastPoint.Tests.Unit
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateOrder_Succeeds_WhenStockIsSufficient()
        {
            var orderRepo = new Mock<IOrderRepository>();
            var userRepo = new Mock<IUserRepository>();
            var prodRepo = new Mock<IProductRepository>();
            var stockRepo = new Mock<IStockRepository>();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var stockId = Guid.NewGuid();

            userRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(new User { Id = userId, Role = UserRole.Client, IsActive = true });
            prodRepo.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(new Product { Id = productId, Price = 10m, IsActive = true });
            stockRepo.Setup(r => r.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Stock, bool>>>()))
                .ReturnsAsync(new List<Stock> { new Stock { Id = stockId, ProductId = productId, Quantity = 5 } });

            var service = new OrderService(orderRepo.Object, userRepo.Object, prodRepo.Object, stockRepo.Object);

            var order = await service.CreateOrderAsync(userId, new List<(Guid, int)> { (productId, 2) });

            Assert.NotNull(order);
            Assert.Equal(20m, order.Total);
        }

        [Fact]
        public async Task CreateOrder_Throws_WhenStockInsufficient()
        {
            var orderRepo = new Mock<IOrderRepository>();
            var userRepo = new Mock<IUserRepository>();
            var prodRepo = new Mock<IProductRepository>();
            var stockRepo = new Mock<IStockRepository>();

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            userRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(new User { Id = userId, Role = UserRole.Client, IsActive = true });
            prodRepo.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(new Product { Id = productId, Price = 10m, IsActive = true });
            stockRepo.Setup(r => r.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Stock, bool>>>()))
                .ReturnsAsync(new List<Stock> { new Stock { Id = Guid.NewGuid(), ProductId = productId, Quantity = 1 } });

            var service = new OrderService(orderRepo.Object, userRepo.Object, prodRepo.Object, stockRepo.Object);

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await service.CreateOrderAsync(userId, new List<(Guid, int)> { (productId, 2) });
            });
        }
    }
}
