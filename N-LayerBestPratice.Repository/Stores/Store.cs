using N_LayerBestPratice.Repository.Products;

namespace N_LayerBestPratice.Repository.Stores;

public class Store
{
    public int Id { get; set; }
    public string StoreName { get; set; }

    public List<Product>? Products { get; set; } 
}