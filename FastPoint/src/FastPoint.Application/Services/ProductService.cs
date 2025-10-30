using FastPoint.Application.Interfaces;
using FastPoint.Domain.Entities;

namespace FastPoint.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IStockRepository _stockRepo;

        public ProductService(IProductRepository productRepo, IStockRepository stockRepo)
        {
            _productRepo = productRepo;
            _stockRepo = stockRepo;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _productRepo.GetAllAsync();

        public async Task<Product?> GetByIdAsync(Guid id) => await _productRepo.GetByIdAsync(id);

        public async Task AddAsync(Product product)
        {
            await _productRepo.AddAsync(product);
            await _productRepo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _productRepo.Update(product);
            await _productRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var prod = await _productRepo.GetByIdAsync(id);
            if (prod == null) throw new Exception("Produto não encontrado.");
            _productRepo.Delete(prod);
            await _productRepo.SaveChangesAsync();
        }
    }
}
