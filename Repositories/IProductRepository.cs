using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductInfo>> GetProductsAsync();

        Task<ProductInfo?> GetProductByRollNoAsync(int id);

        Task<int> AddProductAsync(ProductInfo product);

        Task<bool> updateProductByIdNoAsync(ProductInfo product);

        Task<bool> DeleteProductAsync(int id);
    }
}