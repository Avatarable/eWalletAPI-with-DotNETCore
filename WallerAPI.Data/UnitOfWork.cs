using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data.Repositories.Implementations;
using WallerAPI.Data.Repositories.Interfaces;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WallerDbContext _ctx;

        public UnitOfWork(WallerDbContext ctx)
        {
            _ctx = ctx;
            Wallets = new WalletRepository(_ctx);
            Currencies = new CurrencyRepository(_ctx);
            Transactions = new TransactionRepository(_ctx);
            Photos = new PhotoRepository(_ctx);

        }

        public IWalletRepository Wallets { get; private set; }

        public ICurrencyRepository Currencies { get; private set; }

        public ITransactionRepository Transactions { get; private set; }
        public IPhotoRepository Photos { get; private set; }

        public int Complete()
        {
            return _ctx.SaveChanges();
        }

        public Task<IDbContextTransaction> BeginTransaction()
        {
            return _ctx.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
