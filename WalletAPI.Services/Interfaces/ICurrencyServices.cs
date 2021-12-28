using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Models.Domain;

namespace WallerAPI.Services.Interfaces
{
    public interface ICurrencyServices
    {
        IEnumerable<Currency> GetCurrencies();
        Currency GetCurrencyById(string id);
        Currency GetCurrencyByName(string name);
        Currency AddCurrency(string name, string abbrev);
    }
}
