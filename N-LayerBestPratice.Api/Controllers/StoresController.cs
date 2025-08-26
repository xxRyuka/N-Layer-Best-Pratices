using Microsoft.AspNetCore.Mvc;
using N_LayerBestPratice.Repository.Stores;
using N_LayerBestPratice.Services.Stores;
using N_LayerBestPratice.Services.Stores.Dto.Create;
using N_LayerBestPratice.Services.Stores.Dto.Update;

namespace N_LayerBestPratice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }


    [HttpGet("getStore")]
    public async Task<IActionResult> GetById(int storeId)
    {
        // Working 
        var x = await _storeService.GetStoreByIdAsync(storeId);
        return Ok(x);
    }

    [HttpGet("getStoreWithProducts")]
    public async Task<IActionResult> GetStoreWithProductsById(int storeId)
    {
        var x = await _storeService.GetStoreWithProductsByIdAsync(storeId);
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


    [HttpPut("{storeId:int}")]
    public async Task<IActionResult> UpdateStore(int storeId, UpdateStoreRequest request)
    {
        // await _storeService.UpdateStoreAsync(storeId, request);

        return Ok( await _storeService.UpdateStoreAsync(storeId, request));
    }


    [HttpDelete("{storeId:int}")]
    public async Task<IActionResult> DeleteStore(int storeId)
    {
       
        return Ok( await _storeService.DeleteStoreAsync(storeId));
    }                   
    
    
    
}