using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GainatelieCMS.API.Services;

namespace GainatelieCMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Editor")]
public class MediaController : ControllerBase
{
    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMedia([FromQuery] string? type = null)
    {
        var media = await _mediaService.GetAllMediaAsync(type);
        return Ok(media);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadMedia([FromForm] IFormFile file, [FromForm] string assetType)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded");

        var mediaAsset = await _mediaService.UploadMediaAsync(file, assetType);
        return Ok(mediaAsset);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedia(Guid id)
    {
        await _mediaService.DeleteMediaAsync(id);
        return NoContent();
    }
}
