using GainatelieCMS.API.Models;

namespace GainatelieCMS.API.Services;

public interface IMediaService
{
    Task<List<MediaAsset>> GetAllMediaAsync(string? type = null);
    Task<MediaAsset> UploadMediaAsync(IFormFile file, string assetType);
    Task DeleteMediaAsync(Guid id);
}
