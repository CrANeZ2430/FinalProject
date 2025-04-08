using CloudinaryDotNet.Actions;

namespace SocialNetwork.API.MVC.Services;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}
