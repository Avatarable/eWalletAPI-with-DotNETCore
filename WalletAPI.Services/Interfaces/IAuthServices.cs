using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.DTOs;

namespace WallerAPI.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<LoginCredDTO> Login(string email, string password, bool rememberMe);
    }
}
