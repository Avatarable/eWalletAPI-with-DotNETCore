using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;
using WallerAPI.Models.DTOs;

namespace WallerAPI.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<LoginCredDTO> Login(string email, string password, bool rememberMe);
        Task<string> GenerateEmailConfirmationToken(User user);
        Task<IdentityResult> ConfirmEmail(User user, string token);
        Task<IdentityResult> Register(User user, string password);
    }
}
