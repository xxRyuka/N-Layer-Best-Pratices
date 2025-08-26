using Microsoft.EntityFrameworkCore;
using N_LayerBestPratice.Repository.Concrete;
using N_LayerBestPratice.Repository.DbContext;

namespace N_LayerBestPratice.Repository.Stores;

public class StoreRepository(AppDbContext context) : GenericRepository<Store>(context), IStoreRepository
{
 

    public async Task<Store> GetStoreByIdAsync(int storeId)
    {
        var store = await _context.Stores
            .FirstOrDefaultAsync(x => x.Id == storeId);
        
        return store;
    }

    public Task<Store> GetByIdWithProductsAsync(int storeId)
    {
        var storeWithProducts = _context.Stores
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == storeId);

        return storeWithProducts;
    }

    public IQueryable<Store> GetStoresWithProducts()
    {
        var storesWithProducts = _context.Stores
            .Include(x => x.Products)
            .AsQueryable();
        
        return storesWithProducts;
    }
    
    public IQueryable<Store> GetStores()
    {
        var storesWithProducts = _context.Stores
            .AsQueryable();
        
        return storesWithProducts;
    }
}