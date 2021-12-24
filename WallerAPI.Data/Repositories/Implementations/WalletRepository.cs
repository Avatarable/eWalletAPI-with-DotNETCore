using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WallerAPI.Data.Repositories.Interfaces;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Implementations
{
    public class WalletRepository : CRUDRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(WallerDbContext context) : base(context)
        {
        }

        public Wallet GetWalletByAddress(string address)
        {
            return Ctx.Wallets.FirstOrDefault(c => c.Address == address);
        }

        public IEnumerable<Wallet> GetWalletsByUserId(string userId)
        {
            return Ctx.Wallets.Where(c => c.User.Id == userId).ToList();
        }

        public WallerDbContext Ctx
        {
            get { return Context as WallerDbContext; }
        }
    }
}
