using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductInfo>> GetProductsAsync();

        Task<ProductInfo?> GetProductByRollNoAsync(string rollNo);

        Task<int> AddProductAsync(ProductInfo product);
    }
}