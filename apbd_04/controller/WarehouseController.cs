using apbd_04.service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_04.controller;

[Route("api/warehouse")]
[ApiController]
public class WarehouseController(IWarehouseService _warehouseService) : ControllerBase
{
    [HttpPost]
    public async Task<int> CreateProductWarehouseRecord(ProductWarehouseRequest request)
    {
        return await _warehouseService.CreateProductWarehouseRecord(request);
    }
    
}