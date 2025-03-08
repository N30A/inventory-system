using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;
    
    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSuppliers()
    {
        var result = await _supplierService.GetAllAsync();
        var data = result.Data ?? new List<SupplierDto>();
        
        if (!result.Status)
        {
            return NotFound(new Response<IEnumerable<SupplierDto>>(data, result.Message));
        }
        
        return Ok(new Response<IEnumerable<SupplierDto>>(data));
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSupplierById(int id)
    {
        var result = await _supplierService.GetByIdAsync(id);
        if (!result.Status)
        {
            return NotFound(new Response<SupplierDto?>(result.Data, result.Message));
        }
        
        return Ok(new Response<SupplierDto?>(result.Data));
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetSupplierByName(string name)
    {
        var result = await _supplierService.GetByNameAsync(name);
        if (!result.Status)
        {
            return NotFound(new Response<SupplierDto?>(result.Data, result.Message));
        }
        
        return Ok(new Response<SupplierDto?>(result.Data));
    }
    
    // TODO: object -> correct type
    [HttpPost]
    public async Task<IActionResult> CreateSupplier([FromBody] SupplierDto body) 
    {
        throw new NotImplementedException();
    }
    
    // TODO: object -> correct type
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSupplier(int id, [FromBody] SupplierDto body)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSupplierById(int id)
    {
        throw new NotImplementedException();
    }
}