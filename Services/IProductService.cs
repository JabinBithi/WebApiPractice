using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IProductService
    {
        Task<List<ProductInfo>> GetProductsAsync();

        Task<ProductInfo?> GetProductByRollNoAsync(string rollNo);

        Task<int> AddProductAsync(ProductInfo product);
    }
}