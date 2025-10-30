using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastPoint.Application.Interfaces;
using FastPoint.Application.Services;
using FastPoint.Domain.Entities;
using Moq;
using Xunit;

namespace FastPoint.Tests.Unit
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task AddProduct_ShouldCallRepository()
        {
            var prodRepo = new Mock<IProductRepository>();
            var stockRepo = new Mock<IStockRepository>();

            var service = new ProductService(prodRepo.Object, stockRepo.Object);

            var product = new Product { Id = Guid.NewGuid(), Name = "Test", Price = 10m };

            await service.AddAsync(product);

            prodRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p == product)), Times.Once);
            prodRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnProducts()
        {
            var prodRepo = new Mock<IProductRepository>();
            var stockRepo = new Mock<IStockRepository>();

            prodRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product> { new Product { Id = Guid.NewGuid(), Name = "P1", Price = 1m } });

            var service = new ProductService(prodRepo.Object, stockRepo.Object);

            var result = await service.GetAllAsync();

            Assert.NotEmpty(result);
        }
    }
}
