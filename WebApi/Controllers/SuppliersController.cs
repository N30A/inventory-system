using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
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
            return NotFound(new Response<IEnumerable<SupplierDto>>
            {
                Data = data,
                Message = result.Message
            });
        }
        
        return Ok(new Response<IEnumerable<SupplierDto>> { Data = data });
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSupplierById(int id)
    {
        var result = await _supplierService.GetByIdAsync(id);
        if (!result.Status)
        {
            return NotFound(new Response<SupplierDto?>
            {
                Data = result.Data,
                Message = result.Message
            });
        }
        
        return Ok(new Response<SupplierDto?> { Data = result.Data });
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetSupplierByName(string name)
    {
        var result = await _supplierService.GetByNameAsync(name);
        if (!result.Status)
        {
            return NotFound(new Response<SupplierDto?>
            {
                Data = result.Data,
                Message = result.Message
            });
        }
        
        return Ok(new Response<SupplierDto?> { Data = result.Data } );
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierDto body) 
    {
        if (!ModelState.IsValid)
        {   
            string message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            
            return BadRequest(new Response<CreateSupplierDto>
            {
                Data = body,
                Message = message
            });
        }
        
        var result = await _supplierService.AddAsync(body);
        if (!result.Status)
        {
            return BadRequest(new Response<CreateSupplierDto>
            {
                Data = body,
                Message = result.Message
            });
        }
        
        return CreatedAtAction(nameof(GetSupplierById), new { id = result.Data }, new Response<SupplierDto>
        {
            Data = new SupplierDto
            {
                Id = (int)result.Data!,
                Name = body.Name,
                Address = body.Address,
                Phone = body.Phone,
                Email = body.Email
            }
        });
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
        var deleted = await _supplierService.DeleteAsync(id);
        if (!deleted.Status)
        {
            return NotFound(new Response<SupplierDto?>
            {
                Data = null,
                Message = deleted.Message
            });
        }
        
        return NoContent();
    }
}
