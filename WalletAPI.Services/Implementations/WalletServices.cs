using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Data;
using WallerAPI.Models.Domain;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class WalletServices : IWalletServices
    {
        private readonly IUnitOfWork _work;
        private readonly IUtilServices _util;

        public WalletServices(IUnitOfWork work, IUtilServices util)
        {
            _work = work;
            _util = util;
        }

        public Wallet AddWallet(string currencyName, string userid)
        {
            var currency = _work.Currencies.GetCurrencyByName(currencyName);
            if (currency == null)
            {
                return null;
            }

            var user = _work.Users.Get(userid);
            if (user == null)
            {
                return null;
            }

            var wallet = new Wallet
            {
                Id = Guid.NewGuid().ToString(),
                Currency = currency
            };

            var address = string.Empty;
            do
            {
                address = _util.GenerateAddress();
            } while (GetWalletByAddress(address) != null);

            wallet.Address = address;
            _work.Wallets.Add(wallet);
            _work.Complete();

            return wallet;
        }

        public IEnumerable<Wallet> GetAllWallets()
        {
            return _work.Wallets.GetAll();
        }

        public Wallet GetWalletByAddress(string address)
        {
            return _work.Wallets.GetWalletByAddress(address);
        }
    }
}
