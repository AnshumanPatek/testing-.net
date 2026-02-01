namespace GainatelieCMS.API.DTOs;

public class ProjectListDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? BrandName { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPublished { get; set; }
}

public class ProjectDetailDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? BrandName { get; set; }
    public string? BrandLogoUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? DetailsJson { get; set; }
    public string? HighlightsJson { get; set; }
    public string? DescriptionHtml { get; set; }
    public List<ProjectImageDto> Images { get; set; } = new();
}

public class ProjectDto
{
    public string? Slug { get; set; }
    public string? Title { get; set; }
    public string? BrandName { get; set; }
    public string? DetailsJson { get; set; }
    public string? HighlightsJson { get; set; }
    public string? DescriptionHtml { get; set; }
    public bool IsFeatured { get; set; }
    public int? FeaturedOrder { get; set; }
}

public class ProjectImageDto
{
    public string Url { get; set; } = string.Empty;
    public string? Caption { get; set; }
}
