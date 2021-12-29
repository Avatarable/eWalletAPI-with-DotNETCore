using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Data.Repositories.Implementations;
using WallerAPI.Data.Repositories.Interfaces;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WallerDbContext _ctx;
        private readonly UserManager<User> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public UnitOfWork(WallerDbContext ctx, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _userMgr = userManager;
            _roleMgr = roleManager;
            Users = new UserRepository(_userMgr);
            Roles = new RoleRepository(_roleMgr, _userMgr);
            Wallets = new WalletRepository(_ctx);
            Currencies = new CurrencyRepository(_ctx);
            Transactions = new TransactionRepository(_ctx);

        }

        public IUserRepository Users { get; private set; }

        public IRoleRepository Roles { get; private set; }

        public IWalletRepository Wallets { get; private set; }

        public ICurrencyRepository Currencies { get; private set; }

        public ITransactionRepository Transactions { get; private set; }

        public int Complete()
        {
            return _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
