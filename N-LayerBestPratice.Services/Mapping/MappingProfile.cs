using AutoMapper;
using N_LayerBestPratice.Repository.Products;
using N_LayerBestPratice.Services.Products.Dto;
using N_LayerBestPratice.Services.Products.Dto.Create;
using N_LayerBestPratice.Services.Products.Dto.Update;

namespace N_LayerBestPratice.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        // CreateMap<sourceType,destinationType>()
        CreateMap<Product, ProductDto>()
            .ForCtorParam( nameof(ProductDto.Name),opt=> 
                opt.MapFrom( src => src.Name.ToUpper()+" (Mapped)"));
        
        CreateMap<Product, CreateProductRequest>();
        CreateMap<Product, CreateProductResponse>();

        CreateMap<CreateProductRequest, Product>();
        
        
        
        CreateMap<Product, UpdateProductRequest>();
        
        
    }
}