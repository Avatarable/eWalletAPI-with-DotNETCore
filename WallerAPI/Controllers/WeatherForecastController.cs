using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallerAPI.Data;
using WallerAPI.Models.Domain;

namespace WallerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUnitOfWork _work;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork work)
        {
            _logger = logger;
            _work = work;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpGet("Users")]
        //public IActionResult GetUsers()
        //{
        //    return Ok(_work.Users.GetAll());
        //}

        //[HttpGet("Users/{email}")]
        //public IActionResult GetUser(string email)
        //{
        //    return Ok(_work.Users.GetUserByEmail(email));
        //}

        //[HttpGet("wallets")]
        //public IActionResult GetWallets()
        //{
        //    return Ok(_work.Wallets.GetAll());
        //}

        //[HttpGet("currencies")]
        //public IActionResult GetCurrencies()
        //{
        //    return Ok(_work.Currencies.GetAll());
        //}

        //[HttpPost("currencies")]
        //public IActionResult AddCurrency(string name, string abbrev)
        //{
        //    var currency = new Currency
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Name = name,
        //        Abbreviation = abbrev
        //    };
        //    _work.Currencies.Add(currency);
        //    _work.Complete();
        //    return Ok();
        //}

        //[HttpPost("transaction")]
        //public IActionResult CreateTransaction(decimal amount, TransactionType type, 
        //    TransactionStatus status, string desc, string receivingAddress, string senderAddress)
        //{
        //    var senderWallet = _work.Wallets.GetWalletByAddress(senderAddress);
        //    var receiptWallet = _work.Wallets.GetWalletByAddress(receivingAddress);
        //    if (senderWallet != null && receiptWallet != null)
        //    {
        //        senderWallet.Balance -= amount;
        //        receiptWallet.Balance += amount;
        //    }

        //    var transaction = new Transaction
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Amount = amount,
        //        TransactionType = type,
        //        TransactionStatus = status,
        //        Description = desc,
        //        ContractWalletAddress = receivingAddress,
        //        Wallet = senderWallet
        //    };

        //    _work.Transactions.Add(transaction);
        //    _work.Complete();
        //    return Ok();
        //}

        //[HttpGet("transactions")]
        //public IActionResult GetTransactions()
        //{
        //    return Ok(_work.Transactions.GetAll());
        //}

    }
}
