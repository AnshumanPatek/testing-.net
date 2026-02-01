using System.ComponentModel.DataAnnotations;

namespace GainatelieCMS.API.Models;

public class Page
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required, MaxLength(200)]
    public string Slug { get; set; } = string.Empty; // 404, privacy-policy
    
    [Required, MaxLength(255)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? MetaDescription { get; set; }
    
    // Draft version
    public string? DraftContent { get; set; }
    public DateTime? DraftUpdatedAt { get; set; }
    
    // Published version
    public string? PublishedContent { get; set; }
    public DateTime? PublishedAt { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
