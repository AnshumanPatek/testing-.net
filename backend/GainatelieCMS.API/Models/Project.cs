using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GainatelieCMS.API.Models;

public class Project
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required, MaxLength(200)]
    public string Slug { get; set; } = string.Empty;
    
    [Required, MaxLength(255)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? BrandName { get; set; }
    
    public Guid? BrandLogoId { get; set; }
    [ForeignKey("BrandLogoId")]
    public MediaAsset? BrandLogo { get; set; }
    
    public Guid? ThumbnailId { get; set; }
    [ForeignKey("ThumbnailId")]
    public MediaAsset? Thumbnail { get; set; }
    
    public bool IsFeatured { get; set; }
    public int? FeaturedOrder { get; set; }
    
    // JSON fields
    public string? DetailsJson { get; set; } // [{"label": "Brand", "value": "Erewhon"}]
    public string? HighlightsJson { get; set; } // ["Point 1", "Point 2"]
    public string? DescriptionHtml { get; set; }
    
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
}

public class ProjectImage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid ProjectId { get; set; }
    [ForeignKey("ProjectId")]
    public Project Project { get; set; } = null!;
    
    public Guid AssetId { get; set; }
    [ForeignKey("AssetId")]
    public MediaAsset Asset { get; set; } = null!;
    
    [MaxLength(255)]
    public string? Caption { get; set; }
    
    public int SortOrder { get; set; }
}
