using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WallerAPI.Data;
using WallerAPI.Models.Domain;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class TransactionServices : ITransactionServices
    {
        private readonly IUnitOfWork _work;
        private readonly IConfiguration _config;
        public TransactionServices(IUnitOfWork work, IConfiguration configuration)
        {
            _work = work;
            _config = configuration;
        }


        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _work.Transactions.GetAll();
        }

        public async Task<Transaction> GetTransactionById(string transactionId)
        {
            return await _work.Transactions.Get(transactionId);
        }

        public IEnumerable<Transaction> GetTransactionsByWalletId(string walletId)
        {
            return _work.Transactions.GetTransactionsByWalletId(walletId);
        }

        public async Task<Transaction> Transfer(decimal amount, string description, 
            string receivingWalletAddress, string senderWalletAddress)
        {
            var senderWallet = _work.Wallets.GetWalletByAddress(senderWalletAddress);
            var receivingWallet = _work.Wallets.GetWalletByAddress(receivingWalletAddress);

            var transaction = new Transaction
            {
                Amount = amount,
                TransactionType = default,
                Description = description,
                ContractWalletAddress = receivingWalletAddress,
                Wallet = senderWallet
            };


            using (var transact = await _work.BeginTransaction())
            {
                try
                {
                    if (senderWallet != null && receivingWallet != null)
                    {
                        amount = amount * await ConvertCurrency(amount, senderWallet.Currency.Abbreviation, receivingWallet.Currency.Abbreviation);
                        var transactionDebit = Withdraw(amount, description, senderWalletAddress);
                        var transactionCredit = Deposit(amount, description, receivingWalletAddress);

                        transactionDebit.ContractWalletAddress = receivingWalletAddress;
                        transactionCredit.ContractWalletAddress = senderWalletAddress;

                        _work.Complete();
                        await transact.CommitAsync();
                        transaction.TransactionStatus = TransactionStatus.Successful;
                    }
                }
                catch (Exception)
                {
                    await transact.RollbackAsync();
                    transaction.TransactionStatus = TransactionStatus.Successful;
                }
            }
            return transaction;
        }

        public Transaction Deposit(decimal amount, string description, string walletAddress)
        {
            var wallet = _work.Wallets.GetWalletByAddress(walletAddress);
            var transaction = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                Amount = amount,
                TransactionType = TransactionType.Credit,
                Description = description,
                ContractWalletAddress = "DIRECT DEPOSIT"
            };

            try
            {
                if (wallet != null)
                {
                    wallet.Balance += amount;
                }
                transaction.Wallet = wallet;
                transaction.TransactionStatus = TransactionStatus.Successful;
            }
            catch (Exception)
            {
                transaction.TransactionStatus = TransactionStatus.Declined;
            }
            _work.Transactions.Add(transaction);
            _work.Complete();
            return transaction;
        }

        public Transaction Withdraw(decimal amount, string description, string walletAddress)
        {
            var wallet = _work.Wallets.GetWalletByAddress(walletAddress);
            var transaction = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                Amount = amount,
                TransactionType = TransactionType.Credit,
                Description = description,
                ContractWalletAddress = "DIRECT WITHDRAWAL"
            };

            try
            {
                if (wallet != null)
                {
                    wallet.Balance -= amount;
                }
                transaction.Wallet = wallet;
                transaction.TransactionStatus = TransactionStatus.Successful;
            }
            catch (Exception)
            {
                transaction.TransactionStatus = TransactionStatus.Declined;
            }
            _work.Transactions.Add(transaction);
            _work.Complete();
            return transaction;
        }

        public async Task<decimal> ConvertCurrency(decimal amount, string from, string to)
        {
            string apiKey = _config["CurrencyConverter:apiKey"];
            from = from.Trim();
            to = to.Trim();
            string apiUri = $"api/v7/convert?q={from}_{to}&compact=ultra&apiKey={apiKey}";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://free.currconv.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUri);
                if (response.IsSuccessStatusCode)
                {
                    var conversion = await response.Content.ReadAsStringAsync();
                    var rate = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(conversion).Values.First();
                    return rate;
                    //{"USD_NGN":412.769716}
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
