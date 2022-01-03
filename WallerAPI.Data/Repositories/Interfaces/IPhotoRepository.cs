using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Interfaces
{
    public interface IPhotoRepository : ICRUDRepository<Photo>
    {
        Task<Photo> GetPhotoByPublicId(string PublicId);
        Task<Photo> GetPhotoByUserId(string UserId);
    }
}
