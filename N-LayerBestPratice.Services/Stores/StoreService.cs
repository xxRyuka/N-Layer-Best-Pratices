using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using N_LayerBestPratice.Repository.Stores;
using N_LayerBestPratice.Repository.UnitOfWork;
using N_LayerBestPratice.Services.Products.Dto;
using N_LayerBestPratice.Services.Results;
using N_LayerBestPratice.Services.Stores.Dto;
using N_LayerBestPratice.Services.Stores.Dto.Create;
using N_LayerBestPratice.Services.Stores.Dto.Update;

namespace N_LayerBestPratice.Services.Stores;

/// <summary>
/// Mappers not implemented
/// Results now implementing 
/// </summary>
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

    public async Task<Result<StoreDto<ProductDto>>> GetStoreWithProductsByIdAsync(int storeId) // resulted + mapped 
    {
        // if (storeId is <= 0)
        // {
        //     return Result<StoreDto<ProductDto>>.Failure(ResultStatus.ValidationError, new Error()
        //     {
        //         Message = "Store ID is invalid.",
        //         Code = "InvalidStoreId"
        //     });
        // }

        var entity = await _storeRepository.GetByIdWithProductsAsync(storeId);


        if (entity is null)
        {
            return Result<StoreDto<ProductDto>>.Failure(ResultStatus.NotFound, new Error()
            {
                Message = "Store not found.",
                Code = "StoreNotFound"
            });
        }

        var mapped = _mapper.Map<StoreDto<ProductDto>>(entity);
        return Result<StoreDto<ProductDto>>.Success(mapped);
    }

    public async Task<Result<StoreBaseDto>> GetStoreByIdAsync(int storeId) // resulted + mapped
    {
        // if (storeId is <= 0)
        // {
        //     return Result<StoreBaseDto>.Failure(ResultStatus.NotFound, new Error()
        //     {
        //         Message = "Store ID is invalid.",
        //         Code = "InvalidStoreId"
        //     });
        // }

        var entity = await _storeRepository.GetStoreByIdAsync(storeId);

        if (entity is null)
        {
            return Result<StoreBaseDto>.Failure(ResultStatus.NotFound, new Error()
            {
                Message = "Store not found.",
                Code = "StoreNotFound"
            });
        }


        return Result<StoreBaseDto>.Success(_mapper.Map<StoreBaseDto>(entity));
    }

    public async Task<Result<List<StoreBaseDto>>> GetStoresAsync() // resulted + mapped
    {
        var stores = _storeRepository.GetStores();


        var projectedStores = stores.ProjectTo<StoreBaseDto>(_mapper.ConfigurationProvider);

        return Result<List<StoreBaseDto>>.Success(_mapper.Map<List<StoreBaseDto>>(await projectedStores.ToListAsync()));
    }

    public async Task<Result<List<StoreDto<ProductDto>>>> GetStoresWithProductsAsync() // resulted + mapped 
    {
        var stores = _storeRepository.GetStoresWithProducts()
            .ProjectTo<StoreDto<ProductDto>>(_mapper.ConfigurationProvider);


        return Result<List<StoreDto<ProductDto>>>.Success(await stores.ToListAsync());
    }

    public async Task<Result<CreateStoreResponse>> CreateStoreAsync(CreateStoreRequest request) // resulted + mapped 
    {
        if (request is null)
        {
            return Result<CreateStoreResponse>.Failure(ResultStatus.NotFound, new Error()
            {
                Message = "Request cannot be null.",
                Code = "RequestNull"
            });
        }

        bool exists = await _storeRepository.AnyAsync(x => x.StoreName == request.StoreName);

        if (exists)
        {
            return Result<CreateStoreResponse>.Failure(ResultStatus.ValidationError, new Error()
            {
                Message = "Store with this name already exists.",
                Code = "StoreNameExists"
            });
        }

        Store store = new Store
        {
            StoreName = request.StoreName
        };

        await _storeRepository.AddAsync(store);
        await _unitOfWork.SaveChangesAsync();

        return Result<CreateStoreResponse>.Success(ResultStatus.Created, _mapper.Map<CreateStoreResponse>(store));
    }

    public async Task<Result> UpdateStoreAsync(int storeId, UpdateStoreRequest request) // resulted
    {
        // if (storeId <= 0 || request == null)
        // {
        //     return Result.Failure(ResultStatus.ValidationError, new Error()
        //     {
        //         Message = "Invalid store ID or request.",
        //         Code = "InvalidStoreIdOrRequest"
        //     });
        // }

        bool exists = await _storeRepository.AnyAsync(x => x.StoreName == request.StoreName && x.Id != storeId);

        if (exists)
        {
            return Result.Failure(ResultStatus.ValidationError, new Error()
            {
                Message = "Store with this name already exists.",
                Code = "StoreNameExists"
            });
        }

        var store = await _storeRepository.GetByIdAsync(storeId);

        if (store == null)
        {
            return Result.Failure(ResultStatus.NotFound, new Error()
            {
                Message = "Store not found.",
                Code = "StoreNotFound"
            });
        }

        store.StoreName = request.StoreName;

        _storeRepository.Update(store);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> DeleteStoreAsync(int storeId)
    {
        if (storeId <= 0)
        {
            return Result.Failure(ResultStatus.ValidationError, new Error()
            {
                Message = "Invalid store ID.",
                Code = "InvalidStoreId"
            });
        }

        var store = await _storeRepository.GetByIdAsync(storeId);
        if (store == null)
        {
            return Result.Failure(ResultStatus.NotFound, new Error()
            {
                Message = "Store not found.",
                Code = "StoreNotFound"
            });
        }

        _storeRepository.Delete(store);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}