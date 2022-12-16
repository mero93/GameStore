using API.Helpers;
using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file, ImageType imageType);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
