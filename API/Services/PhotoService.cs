using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file, ImageType imageType)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams();

                if (imageType == ImageType.Square)
                {
                    uploadParams.File = new FileDescription(file.FileName, stream);
                    uploadParams.Transformation = new Transformation().Height(500).Width(500)
                        .Crop("fill").Gravity("face");
                }

                else if (imageType == ImageType.Rectangular)
                {
                    uploadParams.File = new FileDescription(file.FileName, stream);
                    uploadParams.Transformation = new Transformation().Height(233).Width(500)
                        .Crop("fill").Gravity("face");
                }

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
