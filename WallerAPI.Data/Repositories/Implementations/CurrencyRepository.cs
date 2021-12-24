using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Data.Repositories.Interfaces;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Implementations
{
    class CurrencyRepository : CRUDRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(WallerDbContext context) : base(context)
        {
        }
    }
}
