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
        
        //TODO: implement async
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Product WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", id);

        var updatedObjects = cmd.ExecuteNonQuery();
        return updatedObjects > 0;
    }
    
    public async Task<bool> IsWarehouseIdExisting(int id)
    {
        return false;
    }
    
    private string provideConnectionString()
    {
        var connectionString = new SqlConnectionStringBuilder(_configuration["ConnectionStrings:localhostMSSQLServer"]);
        connectionString.UserID = _configuration["DbUserId"];
        connectionString.Password = _configuration["DbPassword"];
        return connectionString.ConnectionString;
    }
}