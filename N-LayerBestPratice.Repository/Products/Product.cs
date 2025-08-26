using N_LayerBestPratice.Repository.Abstract;
using N_LayerBestPratice.Repository.Stores;

namespace N_LayerBestPratice.Repository.Products;

public class Product : IAuditEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Store Store { get; set; }
    public int StoreId { get; set; } // Foreign key for Category
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
}