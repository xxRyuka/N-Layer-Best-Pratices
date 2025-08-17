using Microsoft.AspNetCore.Mvc;
using N_LayerBestPratice.Repository.Stores;
using N_LayerBestPratice.Services.Stores;
using N_LayerBestPratice.Services.Stores.Dto.Create;

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
        var x = await _storeService.GetStoreByIdAsync(storeId);
        return Ok(x);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStore(CreateStoreRequest request)
    {
        var store = await _storeService.CreateStoreAsync(request);
        return Ok(store);
    }
}