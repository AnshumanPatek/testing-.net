using Microsoft.EntityFrameworkCore;
using Amazon.S3;
using Amazon.S3.Transfer;
using GainatelieCMS.API.Data;
using GainatelieCMS.API.Models;

namespace GainatelieCMS.API.Services;

public class MediaService : IMediaService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IAmazonS3 _s3Client;

    public MediaService(AppDbContext context, IConfiguration configuration, IAmazonS3 s3Client)
    {
        _context = context;
        _configuration = configuration;
        _s3Client = s3Client;
    }

    public async Task<List<MediaAsset>> GetAllMediaAsync(string? type = null)
    {
        var query = _context.MediaAssets.AsQueryable();

        if (!string.IsNullOrEmpty(type))
            query = query.Where(m => m.AssetType == type);

        return await query.OrderByDescending(m => m.CreatedAt).ToListAsync();
    }

    public async Task<MediaAsset> UploadMediaAsync(IFormFile file, string assetType)
    {
        var bucketName = _configuration["AWS:S3:BucketName"];
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var key = $"media/{assetType.ToLower()}/{fileName}";

        using var stream = file.OpenReadStream();
        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            Key = key,
            BucketName = bucketName,
            ContentType = file.ContentType
        };

        var transferUtility = new TransferUtility(_s3Client);
        await transferUtility.UploadAsync(uploadRequest);

        var s3Url = $"https://{bucketName}.s3.amazonaws.com/{key}";

        var mediaAsset = new MediaAsset
        {
            FileName = file.FileName,
            S3Key = key,
            S3Url = s3Url,
            MimeType = file.ContentType,
            AssetType = assetType
        };

        _context.MediaAssets.Add(mediaAsset);
        await _context.SaveChangesAsync();

        return mediaAsset;
    }

    public async Task DeleteMediaAsync(Guid id)
    {
        var media = await _context.MediaAssets.FindAsync(id);
        
        if (media != null)
        {
            var bucketName = _configuration["AWS:S3:BucketName"];
            await _s3Client.DeleteObjectAsync(bucketName, media.S3Key);

            _context.MediaAssets.Remove(media);
            await _context.SaveChangesAsync();
        }
    }
}
