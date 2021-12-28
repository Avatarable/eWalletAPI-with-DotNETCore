using System;
using System.Collections.Generic;
using System.Linq;
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

        public Currency GetCurrencyByName(string name)
        {
            return Ctx.Currencies.FirstOrDefault(c => c.Name == name);
        }

        public WallerDbContext Ctx
        {
            get { return Context as WallerDbContext; }
        }
    }
}
