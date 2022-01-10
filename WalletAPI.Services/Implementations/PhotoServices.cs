using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data;
using WallerAPI.Models.Domain;
using WallerAPI.Models.DTOs;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class PhotoServices : IPhotoServices
    {
        private readonly Cloudinary _cloudinary;
        private readonly IUnitOfWork _work;
        private readonly UserManager<User> _userMgr;

        public PhotoServices(IOptions<CloudinarySettings> config,
            IUnitOfWork work, UserManager<User> userMgr)
        {
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
            _work = work;
            _userMgr = userMgr;
        }

        public IEnumerable<Photo> GetAllPhotos()
        {
            return _work.Photos.GetAll();
        }
        public Task<Photo> GetUserPhoto(string userId)
        {
            return _work.Photos.GetPhotoByUserId(userId);
        }
        public async Task<bool> DeleteUserPhoto(string publicId, string userId)
        {
            DeletionParams destroyParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image
            };

            DeletionResult destroyResult = _cloudinary.Destroy(destroyParams);
            if (destroyResult.StatusCode.ToString().Equals("OK"))
            {
                var photo = await _work.Photos.GetPhotoByPublicId(publicId);
                if(photo != null)
                {
                    _work.Photos.Remove(photo);

                    var user = await _userMgr.FindByIdAsync(userId);
                    if (user != null)
                    {
                        user.Photo = null;
                    }

                    if (_work.Complete() > 0) return true;
                }
            }
            return false;
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
        public async Task<bool> AddPhoto(Photo photo, string userId)
        {
            var user = await _userMgr.FindByIdAsync(userId);
            if (user != null)
            {
                photo.User = user;
                _work.Photos.Add(photo);
                if(_work.Complete() > 0) return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserPhoto(Photo model, string userId)
        {
            var user = await _userMgr.FindByIdAsync(userId);
            if (user != null)
            {
                user.Photo = model;
                return true;
            }
            return false;
        }
    }
}
