using System.ComponentModel.DataAnnotations;

namespace Services.Dtos;

public class CreateSupplierDto
{   
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }
    
    [Phone]
    [StringLength(25)]
    public string? Phone { get; set; }
    
    [StringLength(100)]
    public string? Address { get; set; }
}
