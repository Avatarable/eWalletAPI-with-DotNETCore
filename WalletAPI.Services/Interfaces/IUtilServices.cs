using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;

namespace WallerAPI.Services.Interfaces
{
    public interface IUtilServices
    {
        public string GenerateAddress();
    }
}
