using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GainatelieCMS.API.Models;

public class Navbar
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = 1;
    public Guid? LogoId { get; set; }
    [ForeignKey("LogoId")]
    public MediaAsset? Logo { get; set; }
    [MaxLength(100)]
    public string CTAText { get; set; } = "Schedule a call";
    [MaxLength(500)]
    public string? CTAUrl { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class HeroSection
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = 1;
    [MaxLength(255)]
    public string? Headline { get; set; }
    [MaxLength(500)]
    public string? Tagline { get; set; }
    [MaxLength(10)]
    public string BackgroundType { get; set; } = "Image"; // Image or Video
    public Guid? BackgroundId { get; set; }
    [ForeignKey("BackgroundId")]
    public MediaAsset? Background { get; set; }
    public bool IsDraft { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class YouTubeSection
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = 1;
    [MaxLength(500)]
    public string? VideoUrl { get; set; }
    [MaxLength(255)]
    public string? Title { get; set; }
    public bool IsDraft { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class AboutSection
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = 1;
    [MaxLength(255)]
    public string? Title { get; set; }
    public string? Content { get; set; }
    public Guid? ImageId { get; set; }
    [ForeignKey("ImageId")]
    public MediaAsset? Image { get; set; }
    public bool IsDraft { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class WhyChooseUsSection
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = 1;
    [MaxLength(255)]
    public string? Title { get; set; }
    public bool IsDraft { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class WhyChooseUsItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required, MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Description { get; set; }
    public Guid? IconId { get; set; }
    [ForeignKey("IconId")]
    public MediaAsset? Icon { get; set; }
    public int SortOrder { get; set; }
}

public class WhatWeDoSection
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = 1;
    [MaxLength(255)]
    public string? Title { get; set; }
    public bool IsDraft { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class WhatWeDoItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required, MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Description { get; set; }
    public Guid? MediaId { get; set; }
    [ForeignKey("MediaId")]
    public MediaAsset? Media { get; set; }
    [MaxLength(20)]
    public string? MediaType { get; set; } // Image, Video, Icon
    public int SortOrder { get; set; }
}

public class Footer
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = 1;
    [MaxLength(255)]
    public string? Email { get; set; }
    [MaxLength(50)]
    public string? Phone { get; set; }
    [MaxLength(500)]
    public string? Address { get; set; }
    [MaxLength(255)]
    public string Copyright { get; set; } = "© {year} Gainateliê";
    [MaxLength(500)]
    public string? Instagram { get; set; }
    [MaxLength(500)]
    public string? LinkedIn { get; set; }
    [MaxLength(500)]
    public string? Behance { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
