using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Data.Repositories.Interfaces;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Implementations
{
    public class PhotoRepository : CRUDRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(WallerDbContext context) : base(context)
        {
        }
    }
}
