using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;
using WallerAPI.Models.DTOs;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class PhotoServices : IPhotoServices
    {
        private readonly Cloudinary _cloudinary;
        private readonly IPhotoRepository _photoRepo;

        public PhotoServices(IOptions<CloudinarySettings> config,
            IPhotoRepository photoRepo)
        {
            _photoRepo = photoRepo;
            _cloudinary = Cloudinary();
        }

        public Task<IEnumerable<Photo>> GetAllPhotos()
        {
            throw new NotImplementedException();
        }
        public Task<Photo> GetUserPhoto(string userId)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteUserPhoto(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<bool, PhotoUploadDTO>> UploadPhotoAsync(PhotoUploadDTO model, string userId)
        {
            var uploadResult = new ImageUploadResult();

            using (var stream = model.Photo.OpenReadStream())
            {
                var imageUploadParams = new ImageUploadParams
                {
                    File = new FileDescription(model.Photo.FileName, stream),
                    Transformation = new Transformation().Width(100).Height(100)
                };
                uploadResult = await _cloudinary.UploadAsync(imageUploadParams);
            }

            var status = uploadResult.StatusCode.ToString();
            if (status.Equals("OK"))
            {
                model.PublicId = uploadResult.PublicId;
                model.Url = uploadResult.Url.ToString();
                return new Tuple<bool, PhotoUploadDTO>(true, model);
            }

            return new Tuple<bool, PhotoUploadDTO>(true, model);
        }
        public Task<bool> AddPhoto(Photo photo, string userId)
        {
            var res = await _photoRepo.Add(photo);

            return new res;
        }
    }
}
