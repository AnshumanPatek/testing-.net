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
    [HttpGet("hero")]
    public async Task<IActionResult> GetHero()
    {
        var hero = await _contentService.GetHeroSectionAsync();
        return Ok(hero);
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("hero")]
    public async Task<IActionResult> UpdateHero([FromBody] HeroSectionDto dto)
    {
        await _contentService.UpdateHeroSectionAsync(dto);
        return Ok();
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpGet("about")]
    public async Task<IActionResult> GetAbout()
    {
        var about = await _contentService.GetAboutSectionAsync();
        return Ok(about);
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("about")]
    public async Task<IActionResult> UpdateAbout([FromBody] AboutSectionDto dto)
    {
        await _contentService.UpdateAboutSectionAsync(dto);
        return Ok();
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpGet("youtube")]
    public async Task<IActionResult> GetYouTube()
    {
        var youtube = await _contentService.GetYouTubeSectionAsync();
        return Ok(youtube);
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("youtube")]
    public async Task<IActionResult> UpdateYouTube([FromBody] YouTubeSectionDto dto)
    {
        await _contentService.UpdateYouTubeSectionAsync(dto);
        return Ok();
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("navbar")]
    public async Task<IActionResult> UpdateNavbar([FromBody] NavbarDto dto)
    {
        await _contentService.UpdateNavbarAsync(dto);
        return Ok();
    }

    [Authorize(Roles = "Admin,Editor")]
    [HttpPut("footer")]
    public async Task<IActionResult> UpdateFooter([FromBody] FooterDto dto)
    {
        await _contentService.UpdateFooterAsync(dto);
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
