using AutoMapper;
using N_LayerBestPratice.Repository.Products;
using N_LayerBestPratice.Repository.Stores;
using N_LayerBestPratice.Services.Products.Dto;
using N_LayerBestPratice.Services.Products.Dto.Create;
using N_LayerBestPratice.Services.Products.Dto.Update;
using N_LayerBestPratice.Services.Stores.Dto;
using N_LayerBestPratice.Services.Stores.Dto.Create;

namespace N_LayerBestPratice.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<sourceType,destinationType>()
        CreateMap<Product, ProductDto>()
            .ForCtorParam(nameof(ProductDto.Name), opt =>
                opt.MapFrom(src => src.Name.ToUpper() + " (Mapped)"));

        CreateMap<Product, CreateProductRequest>();
        CreateMap<Product, CreateProductResponse>();

        CreateMap<CreateProductRequest, Product>();

        CreateMap<Product, UpdateProductRequest>();


        // Stores Reverse Mapping kullanmicaz bu arada 

        CreateMap<Store, StoreBaseDto>();
        CreateMap<Store, CreateStoreRequest>()
            .ForMember(nameof(CreateStoreRequest.StoreName), opt =>
                opt.MapFrom(src => src.StoreName.ToUpper() + " (Mapped)"));

        CreateMap<CreateStoreRequest, Store>();
        CreateMap<Store, CreateStoreResponse>();
        CreateMap<StoreBaseDto, Store>();
        CreateMap<Store, StoreBaseDto>();

        CreateMap<Store, StoreDto<ProductDto>>()
            .ForMember(x => x.Products,
                opt =>
                    opt.MapFrom(src => src.Products));

        CreateMap<StoreDto<ProductDto>, Store>()
            .ForMember(x => x.Products,
                opt => opt.MapFrom(src => src.Products));
        // CreateMap<, Store>();
    }
}