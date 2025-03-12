using Data.Models;
using Data.Repositories.Interfaces;
using Services.Dtos;
using Services.Interfaces;

namespace Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<Result<IEnumerable<SupplierDto>>> GetAllAsync()
    {   
        var suppliers = (await _supplierRepository.GetAllAsync()).ToList();
        if (suppliers.Count == 0)
        {
            return Result<IEnumerable<SupplierDto>>.Failure("No suppliers were found");
        }
        
        var supplierDtos = suppliers.Select(supplier => new SupplierDto
        {
            Id = supplier.SupplierId,
            Name = supplier.Name,
            Address = supplier.Address,
            Phone = supplier.Phone,
            Email = supplier.Email
        });
        
        return Result<IEnumerable<SupplierDto>>.Success(supplierDtos);
    }

    public async Task<Result<SupplierDto?>> GetByIdAsync(int supplierId)
    {
        var supplier = await _supplierRepository.GetByIdAsync(supplierId);
        if (supplier == null)
        {   
            return Result<SupplierDto?>.Failure($"The supplier with id '{supplierId}' was not found");
        }

        var supplierDto = new SupplierDto
        {
            Id = supplier.SupplierId,
            Name = supplier.Name,
            Address = supplier.Address,
            Phone = supplier.Phone,
            Email = supplier.Email
        };
        
        return Result<SupplierDto?>.Success(supplierDto);
    }
    
    public async Task<Result<SupplierDto?>> GetByNameAsync(string supplierName)
    {
        var supplier = await _supplierRepository.GetByNameAsync(supplierName);
        if (supplier == null)
        {   
            return Result<SupplierDto?>.Failure($"The supplier with the name '{supplierName}' was not found");
        }

        var supplierDto = new SupplierDto
        {
            Id = supplier.SupplierId,
            Name = supplier.Name,
            Address = supplier.Address,
            Phone = supplier.Phone,
            Email = supplier.Email
        };
        
        return Result<SupplierDto?>.Success(supplierDto);
    }

    public async Task<Result<int?>> AddAsync(CreateSupplierDto supplierDto)
    {   
        var existingSupplier = await _supplierRepository.GetByNameAsync(supplierDto.Name);
        if (existingSupplier != null)
        {
            return Result<int?>.Failure($"A supplier with name '{supplierDto.Name}' already exists");
        }
        
        var supplier = new Supplier
        {
            Name = supplierDto.Name,
            Address = supplierDto.Address,
            Phone = supplierDto.Phone,
            Email = supplierDto.Email
        };
        
        int? id = await _supplierRepository.AddAsync(supplier);
        if (id == null)
        {
            return Result<int?>.Failure("Failed to add the supplier");
        }
        
        return Result<int?>.Success(id);
    }

    public async Task<Result<bool>> UpdateAsync(SupplierDto supplierDto)
    {
        var existingSupplier = await _supplierRepository.GetByIdAsync(supplierDto.Id);
        if (existingSupplier == null)
        {
            return Result<bool>.Failure($"The supplier with id '{supplierDto.Id}' was not found");
        }
        
        var supplier = new Supplier
        {
            SupplierId = supplierDto.Id,
            Name = supplierDto.Name,
            Address = supplierDto.Address,
            Phone = supplierDto.Phone,
            Email = supplierDto.Email
        };
        
        bool status = await _supplierRepository.UpdateAsync(supplier);
        if (!status)
        {
            return Result<bool>.Failure($"Failed to update supplier with id '{supplierDto.Id}'");
        }
        
        return Result<bool>.Success(status);
    }

    public async Task<Result<bool>> DeleteAsync(int supplierId)
    {
        var existingSupplier = await _supplierRepository.GetByIdAsync(supplierId);
        if (existingSupplier == null)
        {
            return Result<bool>.Failure($"The supplier with id '{supplierId}' was not found");
        }
        
        bool status = await _supplierRepository.DeleteAsync(supplierId);
        if (!status)
        {
            return Result<bool>.Failure($"Failed to delete supplier with id '{supplierId}'");
        }
        
        return Result<bool>.Success(status);
    }
}
