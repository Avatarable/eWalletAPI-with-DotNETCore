using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data.Repositories.Interfaces;

namespace WallerAPI.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IWalletRepository Wallets { get; }
        ICurrencyRepository Currencies { get; }
        ITransactionRepository Transactions { get; }
        IPhotoRepository Photos { get; }
        int Complete();
        Task<IDbContextTransaction> BeginTransaction();
    }
}
