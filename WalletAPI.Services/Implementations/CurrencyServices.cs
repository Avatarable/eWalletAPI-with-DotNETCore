using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Data;
using WallerAPI.Models.Domain;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class CurrencyServices : ICurrencyServices
    {
        private readonly IUnitOfWork _work;
        public CurrencyServices(IUnitOfWork work)
        {
            _work = work;
        }

        public Currency AddCurrency(string name, string abbrev)
        {
            var currency = new Currency
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Abbreviation = abbrev
            };
            _work.Currencies.Add(currency);
            var done = _work.Complete();
            if (done < 1)
            {
                currency = null;
            }
            return currency;
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            return _work.Currencies.GetAll();
        }

        public Currency GetCurrencyById(string id)
        {
            return _work.Currencies.Get(id).Result;
        }

        public Currency GetCurrencyByName(string name)
        {
            return _work.Currencies.GetCurrencyByName(name);
        }
    }
}
