using System.Data;
using Dapper;
using Data.Models;
using Data.Repositories.Interfaces;

namespace Data.Repositories;

public class SuppliersRepository : ISuppliersRepository
{
    private readonly IDbConnection _connection;

    public SuppliersRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        const string query = """
             SELECT SupplierID, Name, Email, Phone, Address
             FROM dbo.Supplier
             WHERE DeletedAt IS NULL;
         """;

        var suppliers = await _connection.QueryAsync<Supplier>(query);
        return suppliers;
    }

    public async Task<Supplier?> GetByIdAsync(int supplierId)
    {
        const string query = """
             SELECT SupplierID, Name, Email, Phone, Address
             FROM dbo.Supplier
             WHERE DeletedAt IS NULL AND SupplierID = @SupplierId;
         """;
        
        var supplier = await _connection.QuerySingleOrDefaultAsync<Supplier>(query, new { SupplierId = supplierId });
        return supplier;
    }

    public async Task<bool> AddAsync(Supplier supplier)
    {
        const string query = """
             INSERT INTO dbo.Supplier(Name, Email, Phone, Address)
             VALUES(@Name, @Email, @Phone, @Address);
         """;
        
        int rowsAffected = await _connection.ExecuteAsync(query, supplier);
        return rowsAffected == 1;
    }

    public async Task<bool> UpdateAsync(Supplier supplier)
    {
        const string query = """
            UPDATE dbo.Supplier
            SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address
            WHERE SupplierID = @SupplierId;
         """;
        
        int rowsAffected = await _connection.ExecuteAsync(query, supplier);
        return rowsAffected == 1;
    }

    public async Task<bool> DeleteAsync(int supplierId)
    {
        const string query = """
             UPDATE dbo.Supplier
             SET DeletedAt = GETDATE()
             WHERE SupplierID = @SupplierId;
         """;
        
        int rowsAffected = await _connection.ExecuteAsync(query, new { SupplierID = supplierId });
        return rowsAffected == 1;
    }
}