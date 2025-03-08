using Services.Dtos;

namespace Services.Interfaces;

public interface ISupplierService
{
    Task<Result<IEnumerable<SupplierDto>>> GetAllAsync();
    Task<Result<SupplierDto?>> GetByIdAsync(int supplierId);
    Task<Result<SupplierDto?>> GetByNameAsync(string supplierName);
    Task<Result<bool>> AddAsync(SupplierDto supplierDto);
    Task<Result<bool>> UpdateAsync(SupplierDto supplierDto);
    Task<Result<bool>> DeleteAsync(int supplierId);
}
