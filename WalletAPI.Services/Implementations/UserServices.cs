using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
        private readonly IUtilServices _util;

        public UserServices(IUnitOfWork work, IUtilServices util)
        {
            _work = work;
            _util = util;
        }

        public async Task<IdentityResult> AddUser(User user, string password)
        {
            return await _work.Users.Register(user, password);
        }

        public async Task<IdentityResult> AddUserToRole(User user, string role)
        {
            return await _work.Users.AddUserToRole(user, role);
        }

        public async Task<IdentityResult> RemoveUserFromRole(User user, string role)
        {
            return await _work.Users.RemoveUserFromRole(user, role);
        }

        public Task<IdentityResult> ConfirmEmail(User user, string token)
        {
            return _util.ConfirmEmail(user, token);
        }

        public Task<string> GenerateEmailConfirmationToken(User user)
        {
            return _util.GenerateEmailConfirmationToken(user);
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
    }
}
