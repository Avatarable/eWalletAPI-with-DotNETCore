using System;
using System.Collections.Generic;
using System.Text;

namespace WallerAPI.Models.Domain
{
    public class Transaction
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string Description { get; set; }
        public string ContractWalletAddress { get; set; }
        public Wallet Wallet { get; set; }
    }

    public enum TransactionType { Credit, Debit }
    public enum TransactionStatus { Successful, Declined, Pending }
}
