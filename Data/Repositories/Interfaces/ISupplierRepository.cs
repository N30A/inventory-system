using Data.Models;

namespace Data.Repositories.Interfaces;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAllAsync();
    Task<Supplier?> GetByIdAsync(int supplierId);
    Task<Supplier?> GetByNameAsync(string supplierName);
    Task<int?> AddAsync(Supplier supplier);
    Task<bool> UpdateAsync(Supplier supplier);
    Task<bool> DeleteAsync(int supplierId);
}