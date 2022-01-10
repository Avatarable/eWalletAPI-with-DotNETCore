using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data;
using WallerAPI.Models.Domain;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class WalletServices : IWalletServices
    {
        private readonly IUnitOfWork _work;
        private readonly IUtilServices _util;
        private readonly UserManager<User> _userMgr;

        public WalletServices(IUnitOfWork work, UserManager<User> userManager, IUtilServices util)
        {
            _work = work;
            _util = util;
            _userMgr = userManager;

        }

        public async Task<Wallet> AddWallet(string currencyName, string userId)
        {
            var currency = _work.Currencies.GetCurrencyByName(currencyName);
            if (currency == null)
            {
                return null;
            }

            var user = await _userMgr.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var wallet = new Wallet
            {
                Id = Guid.NewGuid().ToString(),
                Currency = currency,
                User = user
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
