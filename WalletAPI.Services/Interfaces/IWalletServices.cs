using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;

namespace WallerAPI.Services.Interfaces
{
    public interface IWalletServices
    {
        IEnumerable<Wallet> GetAllWallets();
        Wallet GetWalletByAddress(string address);
        Task<Wallet> AddWallet(string currencyName, string userid);
    }
}
