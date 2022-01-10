using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data;
using WallerAPI.Models.Domain;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userMgr;

        public UserServices(UserManager<User> userManager)
        {
            _userMgr = userManager;
        }


        public async Task<IdentityResult> AddUserToRole(User user, string role)
        {
            return await _userMgr.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddUserClaim(User user, Claim claim)
        {
            return await _userMgr.AddClaimAsync(user, claim);
        }

        public async Task<IdentityResult> RemoveUserFromRole(User user, string role)
        {
            return await _userMgr.RemoveFromRoleAsync(user, role);
        }

        public void ActivateUser(User user)
        {
            user.IsActive = true;
        }

        public void DeactivateUser(User user)
        {
            user.IsActive = false;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userMgr.Users;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userMgr.FindByEmailAsync(email);
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _userMgr.FindByIdAsync(userId);
        }

        public async Task RemoveUser(User user)
        {
            await _userMgr.DeleteAsync(user);
        }
    }
}
