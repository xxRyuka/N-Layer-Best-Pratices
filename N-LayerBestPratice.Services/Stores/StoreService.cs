using AutoMapper;
using N_LayerBestPratice.Repository.Stores;
using N_LayerBestPratice.Repository.UnitOfWork;
using N_LayerBestPratice.Services.Products.Dto;
using N_LayerBestPratice.Services.Stores.Dto;
using N_LayerBestPratice.Services.Stores.Dto.Create;
using N_LayerBestPratice.Services.Stores.Dto.Update;

namespace N_LayerBestPratice.Services.Stores;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StoreService(IStoreRepository storeRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _storeRepository = storeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<StoreDto> GetStoreByIdAsync(int storeId)
    {
        if (storeId is <= 0)
        {
            return null;
        }

        var entity = await _storeRepository.GetStoreByIdAsync(storeId);

        var dto = new StoreDto()
        {
            Id = entity.Id,
            StoreName = entity.StoreName,
            Products = entity.Products?.Select(p => _mapper.Map<ProductDto>(p)).ToList()
            
        };
        return dto;
    }

    public Task<List<StoreDto>> GetStoresWithProductsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CreateStoreResponse> CreateStoreAsync(CreateStoreRequest request)
    {


        Store store = new Store
        {
            StoreName = request.StoreName
        };

        await _storeRepository.AddAsync(store);
        await _unitOfWork.SaveChangesAsync();
        return new CreateStoreResponse
        {
            StoreName = store.StoreName,
            Id = store.Id
        };
    }

    public Task UpdateStoreAsync(int storeId, UpdateStoreRequest request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteStoreAsync(int storeId)
    {
        throw new NotImplementedException();
    }
}