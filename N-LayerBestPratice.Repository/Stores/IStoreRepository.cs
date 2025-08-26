using N_LayerBestPratice.Repository.Abstract;
using N_LayerBestPratice.Repository.Products;

namespace N_LayerBestPratice.Repository.Stores;

public interface IStoreRepository : IGenericRepository<Store,int>
{
    Task<Store> GetStoreByIdAsync(int storeId);
    Task<Store> GetByIdWithProductsAsync(int storeId);
    IQueryable<Store> GetStoresWithProducts();

    IQueryable<Store> GetStores();

}