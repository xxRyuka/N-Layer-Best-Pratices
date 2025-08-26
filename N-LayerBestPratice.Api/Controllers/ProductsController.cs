using Microsoft.AspNetCore.Mvc;
using N_LayerBestPratice.Repository.Products;
using N_LayerBestPratice.Services.Filters;
using N_LayerBestPratice.Services.Products;
using N_LayerBestPratice.Services.Products.Dto.Create;
using N_LayerBestPratice.Services.Products.Dto.Update;
using N_LayerBestPratice.Services.Products.Dto.UpdateProductStock;

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
    
    [ServiceFilter(typeof(NotFoundFilter<Product,int>))]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id) => 
        CreateActionResult(await _productService.GetProductByIdAsync(id));
    
    [HttpPost("product")]
    public async Task<IActionResult> CreateProduct( CreateProductRequest? request) => 
        CreateActionResult(await _productService.CreateProductAsync(request));
    
    
    
    [ServiceFilter(typeof(NotFoundFilter<Product,int>))]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int? id,  UpdateProductRequest? request) => 
        CreateActionResult(await _productService.UpdateProductAsync(id, request));
    
    [HttpPatch("{id:int}/stock")]
    public async Task<IActionResult> UpdateProductStock([FromBody]UpdateProductStockRequest request) => 
        CreateActionResult(await _productService.UpdateProductStockAsync(request));
    
    [ServiceFilter(typeof(NotFoundFilter<Product,int>))]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int? id) => 
        CreateActionResult(await _productService.DeleteProductAsync(id));
}