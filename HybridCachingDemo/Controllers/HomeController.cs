using HybridCachingDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HybridCachingDemo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly CancellationToken _token;

        public HomeController(IProductService productService)
        {
            _productService = productService;
            _token = new CancellationToken();
        }

        [HttpGet("Products")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductAsync(_token);
            return Ok(products);
        }
    }
}
