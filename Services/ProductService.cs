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

        public async Task<ProductInfo?> GetProductByRollNoAsync(int id)
        {
            return await _repository.GetProductByRollNoAsync(id);
        }

        public async Task<int> AddProductAsync(ProductInfo product)
        {
            return await _repository.AddProductAsync(product);
        }
        public async Task<bool> UpdateProductByIdNoAsync(ProductInfo product)
        {
            return await _repository.updateProductByIdNoAsync(product);
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteProductAsync(id);
        }

    }
}