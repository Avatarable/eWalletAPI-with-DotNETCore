using System;
using System.Collections.Generic;
using System.Text;

namespace WallerAPI.Models.Domain
{
    public class Wallet
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public User User { get; set; }
        public Currency Currency { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public Wallet()
        {
            Transactions = new List<Transaction>();
            Balance = 0.00M;
        }
    }
}
