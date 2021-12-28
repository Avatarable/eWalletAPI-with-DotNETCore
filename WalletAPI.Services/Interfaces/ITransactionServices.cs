using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Models.Domain;

namespace WallerAPI.Services.Interfaces
{
    public interface ITransactionServices
    {
        Transaction CreateTransaction(decimal amount, TransactionType type,
            string desc, string receivingAddress, string senderAddress);
        Transaction GetTransaction(string transactionId);
        IEnumerable<Transaction> GetAllTransactions();
        IEnumerable<Transaction> GetTransactionsByWalletId(string walletId);
    }
}
