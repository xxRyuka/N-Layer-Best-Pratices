using N_LayerBestPratice.Services.Products.Dto;

namespace N_LayerBestPratice.Services.Stores.Dto;

public class StoreBaseDto
{
    public int Id { get; set; }
    public string StoreName { get; set; }

}

public class StoreDto<TProduct> : StoreBaseDto
{
    public List<TProduct>? Products { get; set; } 

}