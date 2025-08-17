using N_LayerBestPratice.Services.Products.Dto;

namespace N_LayerBestPratice.Services.Stores.Dto;

public class StoreDto
{
    public int Id { get; set; }
    public string StoreName { get; set; }

    public List<ProductDto>? Products { get; set; } 
}