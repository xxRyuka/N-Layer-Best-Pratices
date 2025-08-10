using N_LayerBestPratice.Services.Products.Dto;
using N_LayerBestPratice.Services.Products.Dto.Create;
using N_LayerBestPratice.Services.Products.Dto.Update;
using N_LayerBestPratice.Services.Results;

namespace N_LayerBestPratice.Services.Products;

public interface IProductService
{

    Task<Result<List<ProductDto>>> GetAllProductsAsync();
    
    Task<Result<List<ProductDto>>> GetPagedProductsAsync(int pageNumber, int pageSize);
    Task<Result<List<ProductDto>>> GetTopPriceProductsAsync(int count);
    
    Task<Result<ProductDto>> GetProductByIdAsync(int id);
    
    Task<Result<CreateProductResponse>> CreateProductAsync(CreateProductRequest? request);
    
    Task<Result> UpdateProductAsync(int? id, UpdateProductRequest? request);
    
    Task<Result> UpdateProductStockAsync(UpdateProductStockRequest? request);
    
    Task<Result> DeleteProductAsync(int? id);
}