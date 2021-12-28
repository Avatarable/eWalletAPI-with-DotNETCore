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
        private readonly UserManager<User> _userMgr;

        public UtilServices(UserManager<User> userMgr)
        {
            _userMgr = userMgr;
        }

        public string GenerateAddress()
        {
            Random _random = new Random();
            var randInt = _random.Next(1111111111, 1999999999);
            return randInt.ToString();
        }

        public async Task<string> GenerateEmailConfirmationToken(User user)
        {
            return await _userMgr.GenerateEmailConfirmationTokenAsync(user);
        }
        public async Task<IdentityResult> ConfirmEmail(User user, string token)
        {
            return await _userMgr.ConfirmEmailAsync(user, token);
        }
    }
}
