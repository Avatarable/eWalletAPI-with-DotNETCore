using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Interfaces
{
    public interface ITransactionRepository : ICRUDRepository<Transaction>
    {
        IEnumerable<Transaction> GetTransactionsByWalletId(string id);
    }
}
