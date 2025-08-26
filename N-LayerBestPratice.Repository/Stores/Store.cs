using N_LayerBestPratice.Repository.Abstract;
using N_LayerBestPratice.Repository.Concrete;
using N_LayerBestPratice.Repository.Products;

namespace N_LayerBestPratice.Repository.Stores;

public class Store : BaseEntity<int>,IAuditEntity
{
    // public int Id { get; set; }
    public string StoreName { get; set; }

    public List<Product>? Products { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
}