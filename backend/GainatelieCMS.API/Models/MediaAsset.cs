using System.ComponentModel.DataAnnotations;

namespace GainatelieCMS.API.Models;

public class MediaAsset
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required, MaxLength(255)]
    public string FileName { get; set; } = string.Empty;
    
    [Required, MaxLength(500)]
    public string S3Key { get; set; } = string.Empty;
    
    [Required, MaxLength(500)]
    public string S3Url { get; set; } = string.Empty;
    
    [Required, MaxLength(100)]
    public string MimeType { get; set; } = string.Empty;
    
    [Required, MaxLength(20)]
    public string AssetType { get; set; } = string.Empty; // Image, Video, GIF
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
