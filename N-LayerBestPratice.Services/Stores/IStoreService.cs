using N_LayerBestPratice.Services.Stores.Dto;
using N_LayerBestPratice.Services.Stores.Dto.Create;
using N_LayerBestPratice.Services.Stores.Dto.Update;

namespace N_LayerBestPratice.Services.Stores;

public interface IStoreService
{
    Task<StoreDto> GetStoreByIdAsync(int storeId); // REFACTOR EDİLECEK
    Task<List<StoreDto>> GetStoresWithProductsAsync(); // REFACTOR EDİLECEK
    Task<CreateStoreResponse> CreateStoreAsync(CreateStoreRequest request); // REFACTOR EDİLECEK
    Task UpdateStoreAsync(int storeId, UpdateStoreRequest request); // REFACTOR EDİLECEK
    Task DeleteStoreAsync(int storeId); // REFACTOR EDİLECEK
}