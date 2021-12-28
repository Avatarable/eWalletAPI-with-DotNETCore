using System;
using System.Collections.Generic;
using System.Text;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Interfaces
{
    public interface ICurrencyRepository : ICRUDRepository<Currency>
    {
        Currency GetCurrencyByName(string name);
    }
}
