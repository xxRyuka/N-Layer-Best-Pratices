using N_LayerBestPratice.Services.Products.Dto;
using N_LayerBestPratice.Services.Results;
using N_LayerBestPratice.Services.Stores.Dto;
using N_LayerBestPratice.Services.Stores.Dto.Create;
using N_LayerBestPratice.Services.Stores.Dto.Update;

namespace N_LayerBestPratice.Services.Stores;

// result patten implement edelim
public interface IStoreService
{
    Task<Result<StoreDto<ProductDto>>> GetStoreWithProductsByIdAsync(int storeId); // REFACTOR EDİLECEK
    Task<Result<StoreBaseDto>> GetStoreByIdAsync(int storeId); // REFACTOR EDİLECEK  isimleride değiştir

    
    Task<Result<List<StoreBaseDto>>> GetStoresAsync(); // REFACTOR EDİLECEK
    Task<Result<List<StoreDto<ProductDto>>>> GetStoresWithProductsAsync(); // REFACTOR EDİLECEK
    
    
    
    Task<Result<CreateStoreResponse>> CreateStoreAsync(CreateStoreRequest request); // REFACTOR EDİLECEK
    Task<Result> UpdateStoreAsync(int storeId, UpdateStoreRequest request); // REFACTOR EDİLECEK
    Task<Result> DeleteStoreAsync(int storeId); // REFACTOR EDİLECEK
}