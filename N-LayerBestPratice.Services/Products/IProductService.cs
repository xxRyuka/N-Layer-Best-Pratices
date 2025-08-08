using N_LayerBestPratice.Repository.Products;
using N_LayerBestPratice.Services.Results;

namespace N_LayerBestPratice.Services.Products;

public interface IProductService
{

    Task<Result<List<ProductDto>>> GetTopPriceProductsAsync(int count);
}