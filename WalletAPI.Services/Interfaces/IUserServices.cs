﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Models.Domain;

namespace WallerAPI.Services.Interfaces
{
    public interface IUserServices
    {
        IEnumerable<User> GetAllUsers();
        Task<User> GetUserById(string userId);
        Task<User> GetUserByEmail(string email);
        Task<IdentityResult> AddUser(User user, string password);
        Task<IdentityResult> AddUserToRole(User user, string role);
        Task<IdentityResult> AddUserClaim(User user, Claim claim);
        Task<IdentityResult> RemoveUserFromRole(User user, string role);
        Task<string> GenerateEmailConfirmationToken(User user);
        Task<IdentityResult> ConfirmEmail(User user, string token);
        public void ActivateUser(User user);
        public void DeactivateUser(User user);
        void RemoveUser(User user);

    }
}
