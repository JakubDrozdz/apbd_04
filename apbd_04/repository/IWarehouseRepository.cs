namespace apbd_04.repository;

public interface IWarehouseRepository
{
    Task<int> CreateProductWarehouseRecord(ProductWarehouseRequest request);

    Task<bool> IsProductIdExisting(int id);
    
    Task<bool> IsWarehouseIdExisting(int id);
}