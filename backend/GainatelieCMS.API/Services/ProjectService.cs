using Microsoft.EntityFrameworkCore;
using GainatelieCMS.API.Data;
using GainatelieCMS.API.DTOs;
using GainatelieCMS.API.Models;

namespace GainatelieCMS.API.Services;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectListDto>> GetPublishedProjectsAsync()
    {
        return await _context.Projects
            .Where(p => p.IsPublished)
            .Include(p => p.Thumbnail)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new ProjectListDto
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                BrandName = p.BrandName,
                ThumbnailUrl = p.Thumbnail!.S3Url,
                IsFeatured = p.IsFeatured
            })
            .ToListAsync();
    }

    public async Task<List<ProjectListDto>> GetFeaturedProjectsAsync()
    {
        return await _context.Projects
            .Where(p => p.IsPublished && p.IsFeatured)
            .Include(p => p.Thumbnail)
            .OrderBy(p => p.FeaturedOrder)
            .Select(p => new ProjectListDto
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                BrandName = p.BrandName,
                ThumbnailUrl = p.Thumbnail!.S3Url,
                IsFeatured = p.IsFeatured
            })
            .ToListAsync();
    }

    public async Task<List<ProjectListDto>> GetAllProjectsAsync()
    {
        return await _context.Projects
            .Include(p => p.Thumbnail)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new ProjectListDto
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                BrandName = p.BrandName,
                ThumbnailUrl = p.Thumbnail != null ? p.Thumbnail.S3Url : null,
                IsFeatured = p.IsFeatured,
                IsPublished = p.IsPublished
            })
            .ToListAsync();
    }

    public async Task<ProjectDetailDto?> GetProjectBySlugAsync(string slug)
    {
        var project = await _context.Projects
            .Include(p => p.BrandLogo)
            .Include(p => p.Thumbnail)
            .Include(p => p.Images)
                .ThenInclude(i => i.Asset)
            .FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished);

        if (project == null)
            return null;

        return new ProjectDetailDto
        {
            Id = project.Id,
            Slug = project.Slug,
            Title = project.Title,
            BrandName = project.BrandName,
            BrandLogoUrl = project.BrandLogo?.S3Url,
            ThumbnailUrl = project.Thumbnail?.S3Url,
            DetailsJson = project.DetailsJson,
            HighlightsJson = project.HighlightsJson,
            DescriptionHtml = project.DescriptionHtml,
            Images = project.Images.OrderBy(i => i.SortOrder).Select(i => new ProjectImageDto
            {
                Url = i.Asset.S3Url,
                Caption = i.Caption
            }).ToList()
        };
    }

    public async Task<ProjectDetailDto> CreateProjectAsync(ProjectDto dto)
    {
        var project = new Project
        {
            Slug = dto.Slug!,
            Title = dto.Title!,
            BrandName = dto.BrandName,
            DetailsJson = dto.DetailsJson,
            HighlightsJson = dto.HighlightsJson,
            DescriptionHtml = dto.DescriptionHtml,
            IsPublished = false
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return await GetProjectBySlugAsync(project.Slug) ?? new ProjectDetailDto();
    }

    public async Task UpdateProjectAsync(Guid id, ProjectDto dto)
    {
        var project = await _context.Projects.FindAsync(id);
        
        if (project == null)
            throw new KeyNotFoundException("Project not found");

        project.Title = dto.Title!;
        project.BrandName = dto.BrandName;
        project.DetailsJson = dto.DetailsJson;
        project.HighlightsJson = dto.HighlightsJson;
        project.DescriptionHtml = dto.DescriptionHtml;
        project.IsFeatured = dto.IsFeatured;
        project.FeaturedOrder = dto.FeaturedOrder;
        project.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteProjectAsync(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);
        
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }

    public async Task PublishProjectAsync(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);
        
        if (project != null)
        {
            project.IsPublished = true;
            project.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
