using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;
using WallerAPI.Models.DTOs;

namespace WallerAPI.Services.Interfaces
{
    public interface IPhotoServices
    {
        Task<Tuple<bool, PhotoUploadDTO>> UploadPhotoAsync(PhotoUploadDTO model, string userId);
        Task<bool> UpdateUserPhoto(Photo model, string userId);
        Task<bool> AddPhoto(Photo photo, string userId);
        IEnumerable<Photo> GetAllPhotos();
        Task<Photo> GetUserPhoto(string userId);
        Task<bool> DeleteUserPhoto(string publicId, string userId);
    }
}
