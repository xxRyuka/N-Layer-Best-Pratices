using Microsoft.EntityFrameworkCore;
using N_LayerBestPratice.Repository.Concrete;
using N_LayerBestPratice.Repository.DbContext;

namespace N_LayerBestPratice.Repository.Products;

public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    // yeni eklenen özelllikle artı ctoru class basında tanımlayabiliyoruz


    public IQueryable<Product> GetTopPriceProducts(int count)
    {
        return _dbSet
            .OrderByDescending(p => p.Price)
            .Take(count);
        // .ToListAsync();
    }
}