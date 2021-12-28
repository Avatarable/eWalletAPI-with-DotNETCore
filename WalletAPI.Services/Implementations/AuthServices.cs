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
        private readonly IUnitOfWork _work;

        public AuthServices(IJWTServices jwtServices, SignInManager<User> signinMgr, IUnitOfWork work)
        {
            _jwtServices = jwtServices;
            _signinMgr = signinMgr;
            _work = work;
        }

        public async Task<LoginCredDTO> Login(string email, string password, bool rememberMe)
        {
            var user = await _work.Users.GetUserByEmail(email);
            var res = await _signinMgr.PasswordSignInAsync(user, password, rememberMe, false);
            if (!res.Succeeded)
            {
                return new LoginCredDTO { Status = false };
            }

            var userRoles = await _work.Roles.GetUserRoles(user);
            var token = _jwtServices.GenerateToken(user, userRoles.ToList());

            return new LoginCredDTO { Status = true, Id = user.Id, Token = token };
        }
    }
}
