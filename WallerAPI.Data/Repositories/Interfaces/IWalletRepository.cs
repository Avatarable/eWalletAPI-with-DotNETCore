using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Interfaces
{
    public interface IWalletRepository : ICRUDRepository<Wallet>
    {
        Wallet GetWalletByAddress(string address);
        IEnumerable<Wallet> GetWalletsByUserId(string userId);
    }
}
