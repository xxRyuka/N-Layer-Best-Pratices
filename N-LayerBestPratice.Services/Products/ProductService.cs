using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using N_LayerBestPratice.Repository.Products;
using N_LayerBestPratice.Repository.UnitOfWork;
using N_LayerBestPratice.Services.Products.Dto;
using N_LayerBestPratice.Services.Products.Dto.Create;
using N_LayerBestPratice.Services.Products.Dto.Update;
using N_LayerBestPratice.Services.Results;
using AutoMapper.QueryableExtensions;
using N_LayerBestPratice.Services.ExceptionHandlers;
using N_LayerBestPratice.Services.Products.Dto.UpdateProductStock;

namespace N_LayerBestPratice.Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;


    public async Task<Result<List<ProductDto>>> GetAllProductsAsync()
    {
        // Testini yaptim istediğim gibi sonuç donduruyor artık ^^ 
        // throw new CriticalException("This is a critical exception in ProductService.GetAllProductsAsync");
        
        var listQuery = _productRepository.GetAll(trackChanges: false);

        // var mappedList = _mapper.Map<List<ProductDto>> (await listQuery.ToListAsync());
        
        // Alternatively, you can use ProjectTo for better performance with IQueryable 
        
        
        // query.ProjectTo<TDestination>(IConfigurationProvider configuration, bool disableCache = false) seklinde parametreye sahip 
        // ProjectTo, AutoMapper'ın IQueryable üzerinde çalışmasını sağlar ve veritabanı sorgusunu optimize eder.
        // Bu sayede, veritabanından sadece gerekli alanlar çekilir ve performans artar.
        // Ayrıca, bu yöntemle veritabanı sorgusu optimize edilir ve gereksiz veriler çekilmez.
        // Bu, özellikle büyük veri setlerinde performansı artırır.
        // verdiğimiz configurationProvider ile AutoMapper'ın profilini kullanarak
        // ProductDto'ya dönüşüm yapar.
        var mappedList = await listQuery.ProjectTo<ProductDto>(_mapper.ConfigurationProvider).ToListAsync();
        // mappedList ile listQuery üzerinden IQueryable<ProductDto> dönüyoruz sonrada ToListAsync ile listeye çeviriyoruz
        
        return Result<List<ProductDto>>.Success(mappedList);
        
    }

    public async Task<Result<List<ProductDto>>> GetPagedProductsAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return (Result<List<ProductDto>>.Failure(ResultStatus.ValidationError, new Error()
            {
                Message = "Page number and page size must be greater than zero",
                Code = nameof(HttpStatusCode.BadRequest)
            }));
        }

        // page number = 2 and page size = 10 means we want items from 11 to 20 bla bla 
        var list = await _productRepository.GetAll(trackChanges: false)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<List<ProductDto>>.Success(list);
    }

    public async Task<Result<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var listQuery =  _productRepository.GetTopPriceProducts(count);

        
        //Manual mapping from Product to ProductDto for now 

        var resultQuery = listQuery.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

    
        return Result<List<ProductDto>>.Success(await resultQuery.ToListAsync());
    }

    public async Task<Result<ProductDto>> GetProductByIdAsync(int id)
    {
        var entity = await _productRepository.GetByIdAsync(id);

        if (entity == null)
        {
            return Result<ProductDto>.Failure(ResultStatus.NotFound, new Error()
            {
                Message = "Product not found",
                Code = nameof(HttpStatusCode.NotFound)
            });
        }

        // Manual mapping from Product to ProductDto for now
        // ProductDto result = new ProductDto(entity.Id, entity.Name, entity.Price, entity.Stock);
        var result = _mapper.Map<ProductDto>(entity);
        return Result<ProductDto>.Success(result);
    }

    public async Task<Result<CreateProductResponse>> CreateProductAsync(CreateProductRequest? request)
    {
        if (request == null)
        {
            return (Result<CreateProductResponse>.Failure(ResultStatus.ValidationError, new Error()
            {
                Message = "Request cannot be null",
                Code = nameof(HttpStatusCode.BadRequest)
            }));
        }

        if(request.StoreId == null)
        {
            return (Result<CreateProductResponse>.Failure(ResultStatus.ValidationError, new Error()
            {
                Message = "StoreId cannot be null",
                Code = nameof(HttpStatusCode.BadRequest)
            }));
        }
        // Bu kodu FluentValidation ile de yapabiliriz fakat orda yaptiğimiz kod senkron bir şekilde çalışıyor 
        // Veritabaniyla etkileşimde bulunurken asenkron bir şekilde çalışmak daha iyi performans sağlar
        // İslemleri bloklamaz :))
        bool nameExits = await _productRepository.Where(x => x.Name == request.Name, trackChanges: false).AnyAsync();
        if (nameExits)
        {
            return Result<CreateProductResponse>.Failure(ResultStatus.ValidationError, new Error()
            {
                Message = "Product with this name already exists",
                Code = nameof(HttpStatusCode.Conflict)
            });
        }
        
        var entity = _mapper.Map<Product>(request); 
        await _productRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        var responseDto = _mapper.Map<CreateProductResponse>(entity);
        
        return Result<CreateProductResponse>
            .Success(status:ResultStatus.Created,responseDto); // Created response
    }

    public async Task<Result> UpdateProductAsync(int? id, UpdateProductRequest? request)
    {
        var entity = await _productRepository.GetByIdAsync(id.Value);
        if (entity == null)
        {
            return Result.Failure(ResultStatus.NotFound, new Error()
            {
                Message = "Product not found",
                Code = nameof(HttpStatusCode.NotFound)
            });
        }

        entity.Name = request.Name;
        entity.Price = request.Price;
        entity.Stock = request.Stock;
        _productRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success(ResultStatus.NoContent); // No content response
    }

    public async Task<Result> UpdateProductStockAsync(UpdateProductStockRequest request)
    {
        if (request.id == null)
        {
            return (Result.Failure(ResultStatus.ValidationError, new Error()
            {
                Message = "Id cannot be null",
                Code = nameof(HttpStatusCode.BadRequest)
            }));
        }

        var entity = await _productRepository.GetByIdAsync(request.id.Value);
        if (entity == null)
        {
            return (Result.Failure(ResultStatus.NotFound, new Error()
            {
                Message = "Product not found",
                Code = nameof(HttpStatusCode.NotFound)
            }));
        }

        entity.Stock = request.stockQuantity;
        _productRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success(ResultStatus.NoContent);
    }

    public async Task<Result> DeleteProductAsync(int? id)
    {
        if (id == null)
        {
            return Result.Failure(ResultStatus.NotFound, new Error()
            {
                Message = "Id cannot be null",
                Code = nameof(HttpStatusCode.BadRequest)
            });
        }

        var entity = await _productRepository.GetByIdAsync(id.Value);
        if (entity == null)
        {
            return Result.Failure(ResultStatus.NotFound, new Error()
            {
                Message = "Product not found",
                Code = nameof(HttpStatusCode.NotFound)
            });
        }

        _productRepository.Delete(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success(ResultStatus.NoContent);
    }
}