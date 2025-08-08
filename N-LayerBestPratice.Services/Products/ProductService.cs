using N_LayerBestPratice.Repository.Products;
using N_LayerBestPratice.Services.Results;

namespace N_LayerBestPratice.Services.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;


    public async Task<Result<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var list = await _productRepository.GetTopPriceProducts(count);

        
        
        //Manual mapping from Product to ProductDto for now 
        
        List<ProductDto> result = list
            .Select(p => new ProductDto(p.Id,p.Name ,p.Price, p.Stock))
            .ToList();
        
        
        return Result<List<ProductDto>>.Success(result); 
    }
}