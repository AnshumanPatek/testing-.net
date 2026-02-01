using GainatelieCMS.API.DTOs;

namespace GainatelieCMS.API.Services;

public interface IProjectService
{
    Task<List<ProjectListDto>> GetPublishedProjectsAsync();
    Task<List<ProjectListDto>> GetFeaturedProjectsAsync();
    Task<List<ProjectListDto>> GetAllProjectsAsync();
    Task<ProjectDetailDto?> GetProjectBySlugAsync(string slug);
    Task<ProjectDetailDto> CreateProjectAsync(ProjectDto dto);
    Task UpdateProjectAsync(Guid id, ProjectDto dto);
    Task DeleteProjectAsync(Guid id);
    Task PublishProjectAsync(Guid id);
}
