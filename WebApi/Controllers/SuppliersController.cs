using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class SuppliersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetSuppliers()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{supplierId:int}")]
    public IActionResult GetSupplierById(int supplierId)
    {
        throw new NotImplementedException();
    }
    
    // TODO: object -> correct type
    [HttpPost]
    public IActionResult CreateSupplier([FromBody] object body) 
    {
        throw new NotImplementedException();
    }
    
    // TODO: object -> correct type
    [HttpPut("{supplierId:int}")]
    public IActionResult UpdateSupplier(int supplierId, [FromBody] object body)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{supplierId:int}")]
    public IActionResult DeleteSupplierById(int supplierId)
    {
        throw new NotImplementedException();
    }
}