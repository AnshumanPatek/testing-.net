using System.ComponentModel.DataAnnotations;

namespace GainatelieCMS.API.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required, MaxLength(255)]
    public string Email { get; set; } = string.Empty;
    
    [Required, MaxLength(512)]
    public string PasswordHash { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? FirstName { get; set; }
    
    [MaxLength(100)]
    public string? LastName { get; set; }
    
    [Required, MaxLength(20)]
    public string Role { get; set; } = "Editor"; // Admin, CRM, Editor
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
