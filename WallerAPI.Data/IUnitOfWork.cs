using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Data.Repositories.Interfaces;

namespace WallerAPI.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IWalletRepository Wallets { get; }
        ICurrencyRepository Currencies { get; }
        ITransactionRepository Transactions { get; }
        int Complete();
    }
}
