using HybridCachingDemo.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Memory;

namespace HybridCachingDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;
        private readonly HybridCache _hybridCache;
        private readonly IMemoryCache _memoryCache;
        private ILogger<ProductService> _logger;

        public ProductService(AppDbContext appDbContext, HybridCache hybridCache, IMemoryCache memoryCache, ILogger<ProductService> logger)
        {
            _appDbContext = appDbContext;
            _hybridCache = hybridCache;
            _memoryCache = memoryCache;
            _logger = logger;
        }
        public async Task<List<Product>> GetProductAsync(CancellationToken ct)
        {
            var data = new List<Product>();
            var products = await _hybridCache.GetOrCreateAsync("products",
                async ct =>
                {
                    var response = await _appDbContext.Products.ToListAsync(ct);
                    _logger.LogInformation("DB Visited");
                    return response;
                });
            return products;
        }

        public async Task<List<Product>> GetProductAsyncOld(CancellationToken ct)
        {
            return await _appDbContext.Products.ToListAsync(ct);
        }

        public async Task<List<Product>> GetProductAsyncRateLimting(CancellationToken ct)
        {
            return await _appDbContext.Products.ToListAsync(ct);
        }
    }
}
