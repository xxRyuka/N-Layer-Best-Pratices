using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

    public async Task<List<StoreDto>> GetStoresWithProductsAsync()
    { 
        var stores = await _storeRepository.GetStoresWithProducts()
           .Select(s => new StoreDto  // bu nasil bir maplemedir amk neyse refactor edicem bütün servisi en azından mapliyoz 
            {
                Id = s.Id,
                StoreName = s.StoreName,
                Products = s.Products.Select(p => _mapper.Map<ProductDto>(p)).ToList()
            }).ToListAsync();

        return stores;
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

    public async Task UpdateStoreAsync(int storeId, UpdateStoreRequest request)
    {
        if (storeId <= 0 || request == null)
        {
            throw new ArgumentException("Invalid store ID or request.");
        }

        var store = _storeRepository.GetByIdAsync(storeId).Result;

        if (store == null)
        {
            throw new KeyNotFoundException("Store not found.");
        }

        store.StoreName = request.StoreName;

        _storeRepository.Update(store);
        await _unitOfWork.SaveChangesAsync();

        // result patterni implement edeceğim buraya 
    }

    public async Task DeleteStoreAsync(int storeId)
    {
        if (storeId <= 0)
        {
            throw new ArgumentException("Invalid store ID.");
        }
        var store = _storeRepository.GetByIdAsync(storeId).Result;
        if (store == null)
        {
            throw new KeyNotFoundException("Store not found.");
        }
        _storeRepository.Delete(store);
        await _unitOfWork.SaveChangesAsync();
        
    }
}