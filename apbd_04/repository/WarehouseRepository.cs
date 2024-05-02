using System.Data.SqlClient;

namespace apbd_04.repository;

public class WarehouseRepository(IConfiguration _configuration) : IWarehouseRepository
{
    public async Task<int> CreateProductWarehouseRecord(ProductWarehouseRequest request)
    {
        return 0;
    }

    public async Task<bool> IsProductIdExisting(int id)
    {
        await using var con = new SqlConnection(provideConnectionString());
        await con.OpenAsync();
        
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Product WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", id);

        var updatedObjects = cmd.ExecuteNonQueryAsync();
        Task.Delay(150000);
        return await updatedObjects > 0;
    }
    
    public async Task<bool> IsWarehouseIdExisting(int id)
    {
        await using var con = new SqlConnection(provideConnectionString());
        await con.OpenAsync();
        
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        cmd.Parameters.AddWithValue("@IdWarehouse", id);

        var updatedObjects = cmd.ExecuteNonQueryAsync();
        return await updatedObjects > 0;
    }
    
    public async Task<int> IsOrderExisting(int id, int amount, DateTime createDate)
    {
        await using var con = new SqlConnection(provideConnectionString());
        await con.OpenAsync();
        
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdOrder FROM Order WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @CreateDate";
        cmd.Parameters.AddWithValue("@IdProduct", id);
        cmd.Parameters.AddWithValue("@Amount", amount);
        cmd.Parameters.AddWithValue("@CreateDate", createDate);

        using (var dr = await cmd.ExecuteReaderAsync())
        {
            while (await dr.ReadAsync())
            {
                return (int)dr["IdOrder"];
            }
        }
                
        
        return -1;
    }

    public async Task<bool> IsOrderCompleted(int orderId)
    {
        await using var con = new SqlConnection(provideConnectionString());
        await con.OpenAsync();
        
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Product_Warehouse WHERE IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@IdOrder", orderId);

        var updatedObjects = cmd.ExecuteNonQueryAsync();
        return await updatedObjects > 0;
    }
    
    private string provideConnectionString()
    {
        var connectionString = new SqlConnectionStringBuilder(_configuration["ConnectionStrings:localhostMSSQLServer"]);
        connectionString.UserID = _configuration["DbUserId"];
        connectionString.Password = _configuration["DbPassword"];
        return connectionString.ConnectionString;
    }
}