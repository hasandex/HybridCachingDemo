using HybridCachingDemo.Models;

namespace HybridCachingDemo.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductAsync(CancellationToken ct);
        Task<List<Product>> GetProductAsyncOld(CancellationToken ct);
        Task<List<Product>> GetProductAsyncRateLimting(CancellationToken ct);

    }
}
