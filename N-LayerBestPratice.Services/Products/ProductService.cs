using System.Net;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using N_LayerBestPratice.Repository.Products;
using N_LayerBestPratice.Repository.UnitOfWork;
using N_LayerBestPratice.Services.Products.Dto;
using N_LayerBestPratice.Services.Products.Dto.Create;
using N_LayerBestPratice.Services.Products.Dto.Update;
using N_LayerBestPratice.Services.Results;

namespace N_LayerBestPratice.Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task<Result<List<ProductDto>>> GetAllProductsAsync()
    {
        var list = _productRepository.GetAll(trackChanges: false);

        return Result<List<ProductDto>>.Success(await list
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock))
            .ToListAsync());
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
        var list = _productRepository.GetAll(trackChanges: false)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return Result<List<ProductDto>>.Success(await list
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock))
            .ToListAsync());
    }

    public async Task<Result<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var list = await _productRepository.GetTopPriceProducts(count);


        //Manual mapping from Product to ProductDto for now 

        List<ProductDto> result = list
            .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock))
            .ToList();


        return Result<List<ProductDto>>.Success(result);
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

        //Manual mapping from Product to ProductDto for now
        ProductDto result = new ProductDto(entity.Id, entity.Name, entity.Price, entity.Stock);
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

        var entity = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        await _productRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return Result<CreateProductResponse>
            .Success(new
                CreateProductResponse(entity.Id, entity.Name, entity.Price, entity.Stock));
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