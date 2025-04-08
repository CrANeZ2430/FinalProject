using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace SocialNetwork.API.MVC.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;

    public PhotoService(IConfiguration config)
    {
        var account = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResults = new ImageUploadResult();

        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
            };
            uploadResults = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResults;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string photoId)
    {
        var deleteParams = new DeletionParams(photoId);
        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result;
    }
}
