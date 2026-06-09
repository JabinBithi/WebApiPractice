using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IProductService
    {
        Task<List<ProductInfo>> GetProductsAsync();

        Task<ProductInfo?> GetProductByRollNoAsync(int id);

        Task<int> AddProductAsync(ProductInfo product);

        Task<bool> UpdateProductByIdNoAsync(ProductInfo product);
        Task<bool> DeleteProductAsync(int id);
    }
}