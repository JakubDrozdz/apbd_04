using apbd_04.repository;

namespace apbd_04.service;

public class WarehouseService(IWarehouseRepository _warehouseRepository) : IWarehouseService
{
    public async Task<int> CreateProductWarehouseRecord(ProductWarehouseRequest request)
    {
        if (!await ValidateRequest(request))
        {
            return -1;
        }
        return 0;
    }

    private async Task<bool> ValidateRequest(ProductWarehouseRequest request)
    {
        Task<bool> isProductExisting = _warehouseRepository.IsProductIdExisting(request.IdProduct);
        Task<bool> isWarehouseExisting = _warehouseRepository.IsWarehouseIdExisting(request.IdWarehouse);
        Task<int> isOrderExisting =
            _warehouseRepository.IsOrderExisting(request.IdProduct, request.Amount, request.CreatedAt);
        Task<bool> isOrderCompleted = _warehouseRepository.IsOrderCompleted(await isOrderExisting);

        await Task.WhenAll([isProductExisting, isWarehouseExisting, isOrderCompleted]);

        return isProductExisting.Result && isWarehouseExisting.Result && request.Amount > 0 && isOrderCompleted.Result;
    }
}