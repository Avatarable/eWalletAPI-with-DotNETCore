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
        private readonly IUnitOfWork _work;

        public UserServices(IUnitOfWork work)
        {
            _work = work;
        }

        

        public async Task<IdentityResult> AddUserToRole(User user, string role)
        {
            return await _work.Users.AddUserToRole(user, role);
        }

        public async Task<IdentityResult> AddUserClaim(User user, Claim claim)
        {
            return await _work.Users.AddUserClaim(user, claim);
        }

        public async Task<IdentityResult> RemoveUserFromRole(User user, string role)
        {
            return await _work.Users.RemoveUserFromRole(user, role);
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
            return _work.Users.GetAll();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _work.Users.GetUserByEmail(email);
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _work.Users.Get(userId);
        }

        public void RemoveUser(User user)
        {
            _work.Users.Remove(user);
        }
    }
}
