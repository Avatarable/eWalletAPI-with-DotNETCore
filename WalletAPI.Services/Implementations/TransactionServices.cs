using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Data;
using WallerAPI.Models.Domain;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class TransactionServices : ITransactionServices
    {
        private readonly IUnitOfWork _work;
        public TransactionServices(IUnitOfWork work)
        {
            _work = work;
        }

        public Transaction CreateTransaction(decimal amount, TransactionType type, string desc, string receivingAddress, string senderAddress)
        {
            var senderWallet = _work.Wallets.GetWalletByAddress(senderAddress);
            var receiptWallet = _work.Wallets.GetWalletByAddress(receivingAddress);
            Transaction transaction = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                Amount = amount,
                TransactionType = type,
                Description = desc,
                ContractWalletAddress = receivingAddress,
                Wallet = senderWallet
            };

            try
            {
                if (senderWallet != null && receiptWallet != null)
                {
                    senderWallet.Balance -= amount;
                    receiptWallet.Balance += amount;
                }
                transaction.TransactionStatus = TransactionStatus.Successful;
            }
            catch (Exception)
            {
                transaction.TransactionStatus = TransactionStatus.Declined;
            }
            _work.Transactions.Add(transaction);
            return transaction;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _work.Transactions.GetAll();
        }

        public Transaction GetTransaction(string transactionId)
        {
            return _work.Transactions.Get(transactionId).Result;
        }

        public IEnumerable<Transaction> GetTransactionsByWalletId(string walletId)
        {
            return _work.Transactions.GetTransactionsByWalletId(walletId);
        }
    }
}
