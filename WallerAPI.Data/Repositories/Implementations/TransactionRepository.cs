using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WallerAPI.Data.Repositories.Interfaces;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Implementations
{
    public class TransactionRepository : CRUDRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(WallerDbContext context) : base(context)
        {
        }
        public IEnumerable<Transaction> GetTransactionsByWalletId(string id)
        {
            return Ctx.Transactions.Where(c => c.Wallet.Id == id);
        }
        public WallerDbContext Ctx
        {
            get { return Context as WallerDbContext; }
        }
    }
}
