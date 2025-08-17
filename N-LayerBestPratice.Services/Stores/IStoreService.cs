using N_LayerBestPratice.Services.Stores.Dto;
using N_LayerBestPratice.Services.Stores.Dto.Create;
using N_LayerBestPratice.Services.Stores.Dto.Update;

namespace N_LayerBestPratice.Services.Stores;

public interface IStoreService
{
    Task<StoreDto> GetStoreByIdAsync(int storeId);
    Task<List<StoreDto>> GetStoresWithProductsAsync();
    Task<CreateStoreResponse> CreateStoreAsync(CreateStoreRequest request);
    Task UpdateStoreAsync(int storeId, UpdateStoreRequest request);
    Task DeleteStoreAsync(int storeId);
    
}