using Microsoft.AspNetCore.Mvc;
using N_LayerBestPratice.Repository.Stores;
using N_LayerBestPratice.Services.Filters;
using N_LayerBestPratice.Services.Stores;
using N_LayerBestPratice.Services.Stores.Dto.Create;
using N_LayerBestPratice.Services.Stores.Dto.Update;

namespace N_LayerBestPratice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoresController : CustomControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [ServiceFilter(typeof(NotFoundFilter<Store, int>))]
    [HttpGet("getStore")]
    public async Task<IActionResult> GetById(int id)
    {
        // Working 
        var x = await _storeService.GetStoreByIdAsync(id);
        return Ok(x);
    }

    [ServiceFilter(typeof(NotFoundFilter<Store, int>))]
    [HttpGet("getStoreWithProducts")]
    public async Task<IActionResult> GetStoreWithProductsById(int id)
    {
        var x = await _storeService.GetStoreWithProductsByIdAsync(id);
        return Ok(x);
    }


    [HttpGet("getStores")]
    public async Task<IActionResult> GetStores()
    {
        // Working
        var stores = await _storeService.GetStoresAsync();
        return Ok(stores);
    }


    [HttpGet("getStoresWithProducts")]
    public async Task<IActionResult> GetStoresWithProducts()
    {
        var stores = await _storeService.GetStoresWithProductsAsync();
        return Ok(stores);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStore(CreateStoreRequest request)
    {
        var store = await _storeService.CreateStoreAsync(request);
        return Ok(store);
    }

    [ServiceFilter(typeof(NotFoundFilter<Store, int>))]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStore(int id, UpdateStoreRequest request)
    {
        // await _storeService.UpdateStoreAsync(storeId, request);

        return Ok(await _storeService.UpdateStoreAsync(id, request));
    }

    [ServiceFilter(typeof(NotFoundFilter<Store, int>))]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStore(int id)
    {
        return Ok(await _storeService.DeleteStoreAsync(id));
    }
}