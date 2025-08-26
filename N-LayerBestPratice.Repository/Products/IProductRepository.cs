using N_LayerBestPratice.Repository.Abstract;

namespace N_LayerBestPratice.Repository.Products;

public interface IProductRepository : IGenericRepository<Product,int>
{
    // bla bla gibisinden ozel metotlar,
    // bunları servicede özel yazabilirdik fakat sql sorgularını repositoryde yazmak mimari açısından daha mantıklı
    
    // Task<IEnumerable<Product>> GetTopPriceProducts(int count); 

    IQueryable<Product> GetTopPriceProducts(int count);
}
