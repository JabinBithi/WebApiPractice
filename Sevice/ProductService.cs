using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductInfo>> GetProductsAsync()
        {
            return await _repository.GetProductsAsync();
        }

        public async Task<ProductInfo?> GetProductByRollNoAsync(string rollNo)
        {
            return await _repository.GetProductByRollNoAsync(rollNo);
        }

        public async Task<int> AddProductAsync(ProductInfo product)
        {
            return await _repository.AddProductAsync(product);
        }
    }
}