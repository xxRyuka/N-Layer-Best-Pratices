using Microsoft.AspNetCore.Mvc;
using N_LayerBestPratice.Services.Products;
using N_LayerBestPratice.Services.Products.Dto.Create;

namespace N_LayerBestPratice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : CustomControllerBase
{
    private readonly IProductService _productService;


    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("get-all-products")]
    // [HttpGet]
    public async Task<IActionResult> Index() => CreateActionResult(await _productService.GetAllProductsAsync());

    [HttpGet("get-top-price-products")]
    public async Task<IActionResult> GetTopPriceProducts(int count) => CreateActionResult(await _productService.GetTopPriceProductsAsync(count));
    
    [HttpGet("get-product-by-id")]
    public async Task<IActionResult> GetProductById(int id) => CreateActionResult(await _productService.GetProductByIdAsync(id));
    
    [HttpPost("create-product")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest? request) 
        => CreateActionResult(await _productService.CreateProductAsync(request));
}