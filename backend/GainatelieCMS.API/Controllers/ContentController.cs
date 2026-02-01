using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GainatelieCMS.API.Services;
using GainatelieCMS.API.DTOs;

namespace GainatelieCMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentController(IContentService contentService)
    {
        _contentService = contentService;
    }

    // Public endpoints
    [HttpGet("home")]
    public async Task<IActionResult> GetHomeContent()
    {
        var content = await _contentService.GetPublishedHomeContentAsync();
        return Ok(content);
    }

    [HttpGet("navbar")]
    public async Task<IActionResult> GetNavbar()
    {
        var navbar = await _contentService.GetNavbarAsync();
        return Ok(navbar);
    }

    [HttpGet("footer")]
    public async Task<IActionResult> GetFooter()
    {
        var footer = await _contentService.GetFooterAsync();
        return Ok(footer);
    }

    // Admin endpoints
    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("hero")]
    public async Task<IActionResult> UpdateHero([FromBody] HeroSectionDto dto)
    {
        await _contentService.UpdateHeroSectionAsync(dto);
        return Ok();
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("about")]
    public async Task<IActionResult> UpdateAbout([FromBody] AboutSectionDto dto)
    {
        await _contentService.UpdateAboutSectionAsync(dto);
        return Ok();
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpPost("publish/{section}")]
    public async Task<IActionResult> PublishSection(string section)
    {
        await _contentService.PublishSectionAsync(section);
        return Ok();
    }
}
