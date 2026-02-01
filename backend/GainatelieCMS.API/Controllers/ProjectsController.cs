using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GainatelieCMS.API.Services;
using GainatelieCMS.API.DTOs;

namespace GainatelieCMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPublishedProjects()
    {
        var projects = await _projectService.GetPublishedProjectsAsync();
        return Ok(projects);
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetFeaturedProjects()
    {
        var projects = await _projectService.GetFeaturedProjectsAsync();
        return Ok(projects);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetProjectBySlug(string slug)
    {
        var project = await _projectService.GetProjectBySlugAsync(slug);
        
        if (project == null)
            return NotFound();
        
        return Ok(project);
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpGet("admin/all")]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(projects);
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] ProjectDto dto)
    {
        var project = await _projectService.CreateProjectAsync(dto);
        return CreatedAtAction(nameof(GetProjectBySlug), new { slug = project.Slug }, project);
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectDto dto)
    {
        await _projectService.UpdateProjectAsync(id, dto);
        return Ok();
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        await _projectService.DeleteProjectAsync(id);
        return NoContent();
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpPost("{id}/publish")]
    public async Task<IActionResult> PublishProject(Guid id)
    {
        await _projectService.PublishProjectAsync(id);
        return Ok();
    }
}
