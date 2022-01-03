using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data.Repositories.Interfaces;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Implementations
{
    public class PhotoRepository : CRUDRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(WallerDbContext context) : base(context)
        {
        }

        public async Task<Photo> GetPhotoByPublicId(string PublicId)
        {
            return await Ctx.Photos.Include(x => x.User).FirstOrDefaultAsync(x => x.PublicId == PublicId);
        }

        public async Task<Photo> GetPhotoByUserId(string UserId)
        {
            return await Ctx.Photos.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == UserId);
        }

        public WallerDbContext Ctx
        {
            get { return Context as WallerDbContext; }
        }
    }
}
