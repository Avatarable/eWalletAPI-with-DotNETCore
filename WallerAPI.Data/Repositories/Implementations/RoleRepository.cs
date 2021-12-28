using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data.Repositories.Interfaces;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleMgr;
        private readonly UserManager<User> _userMgr;

        public RoleRepository(RoleManager<IdentityRole> roleManager, UserManager<User> userMgr)
        {
            _roleMgr = roleManager;
            _userMgr = userMgr;
        }
        public async Task<IdentityResult> AddRole(string name)
        {
            return await _roleMgr.CreateAsync(new IdentityRole(name));
        }

        public async Task<IList<string>> GetUserRoles(User user)
        {
            return await _userMgr.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RemoveRole(string role)
        {
            var roleToRemove = await _roleMgr.FindByNameAsync(role);
            return await _roleMgr.DeleteAsync(roleToRemove);
        }
    }
}
