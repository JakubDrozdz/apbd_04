using Microsoft.AspNetCore.Mvc;

namespace apbd_04.controller;

[Route("api/warehouse")]
[ApiController]
public class WarehouseController : ControllerBase
{
    [HttpPost]
    public int CreateProductWarehouseRecord(ProductWarehouseRequest request)
    {
        return 0;
    }
    
}