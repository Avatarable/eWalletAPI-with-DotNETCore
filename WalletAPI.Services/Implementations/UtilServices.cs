using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class UtilServices : IUtilServices
    {

        public string GenerateAddress()
        {
            Random _random = new Random();
            var randInt = _random.Next(1111111111, 1999999999);
            return randInt.ToString();
        }
    }
}
