using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using WallerAPI.Data;
using WallerAPI.Models.Domain;
using WallerAPI.Models.DTOs;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class AuthServices : IAuthServices
    {
        private readonly SignInManager<User> _signinMgr;
        private readonly IJWTServices _jwtServices;
        private readonly IUtilServices _util;
        private readonly UserManager<User> _userMgr;

        public AuthServices(IJWTServices jwtServices, SignInManager<User> signinMgr,
            UserManager<User> userManager, IUtilServices util)
        {
            _jwtServices = jwtServices;
            _signinMgr = signinMgr;
            _userMgr = userManager;
            _util = util;
        }

        public async Task<LoginCredDTO> Login(string email, string password, bool rememberMe)
        {
            var user = await _userMgr.FindByEmailAsync(email);
            var res = await _signinMgr.PasswordSignInAsync(user, password, rememberMe, false);

            if (!res.Succeeded)
            {
                return new LoginCredDTO { Status = false };
            }

            var userRoles = await _userMgr.GetRolesAsync(user);
            var claims = await _userMgr.GetClaimsAsync(user);
            var token = _jwtServices.GenerateToken(user, userRoles.ToList(), claims);

            return new LoginCredDTO { Status = true, Id = user.Id, Token = token };
        }

        public async Task<string> GenerateEmailConfirmationToken(User user)
        {
            return await _userMgr.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmail(User user, string token)
        {
            var res = await _userMgr.ConfirmEmailAsync(user, token);
            if (res.Succeeded)
            {
                user.IsActive = true;
            }
            return res;
        }

        public async Task<IdentityResult> Register(User user, string password)
        {
            return await _userMgr.CreateAsync(user, password);
        }
    }
}
