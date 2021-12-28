using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Models.Domain;

namespace WallerAPI.Services.Interfaces
{
    public interface IWalletServices
    {
        IEnumerable<Wallet> GetAllWallets();
        Wallet GetWalletByAddress(string address);
        Wallet AddWallet(string currencyName, string userid);
    }
}
