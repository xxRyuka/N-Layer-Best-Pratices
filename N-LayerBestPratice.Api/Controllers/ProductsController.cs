using Microsoft.AspNetCore.Mvc;
using N_LayerBestPratice.Services.Products;
using N_LayerBestPratice.Services.Products.Dto.Create;
using N_LayerBestPratice.Services.Products.Dto.Update;

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

    [HttpGet("products")]
    public async Task<IActionResult> GelAllProducts() => 
        CreateActionResult(await _productService.GetAllProductsAsync());

    [HttpGet("top-price-products")]
    public async Task<IActionResult> GetTopPriceProducts(int count) => 
        CreateActionResult(await _productService.GetTopPriceProductsAsync(count));
    
    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPagedProducts(int pageNumber, int pageSize) =>
        CreateActionResult(await _productService.GetPagedProductsAsync(pageNumber, pageSize));
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id) => 
        CreateActionResult(await _productService.GetProductByIdAsync(id));
    
    [HttpPost("product")]
    public async Task<IActionResult> CreateProduct( CreateProductRequest? request) => 
        CreateActionResult(await _productService.CreateProductAsync(request));
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int? id,  UpdateProductRequest? request) => 
        CreateActionResult(await _productService.UpdateProductAsync(id, request));
    
    [HttpPatch("{id:int}/stock")]
    public async Task<IActionResult> UpdateProductStock([FromBody]UpdateProductStockRequest request) => 
        CreateActionResult(await _productService.UpdateProductStockAsync(request));
    
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int? id) => 
        CreateActionResult(await _productService.DeleteProductAsync(id));
}