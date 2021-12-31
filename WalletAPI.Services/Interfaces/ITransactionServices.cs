using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;

namespace WallerAPI.Services.Interfaces
{
    public interface ITransactionServices
    {
        Task<Transaction> GetTransactionById(string transactionId);
        IEnumerable<Transaction> GetAllTransactions();
        IEnumerable<Transaction> GetTransactionsByWalletId(string walletId);
        Task<Transaction> Transfer(decimal amount, string description, string receivingAddress, 
            string senderAddress);
        Transaction Deposit(decimal amount, string description, string walletAddress);
        Transaction Withdraw(decimal amount, string description, string walletAddress);

    }
}
