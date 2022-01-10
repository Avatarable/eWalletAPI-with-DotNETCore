using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Services.Implementations
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<IdentityRole> _roleMgr;
        public RoleServices(RoleManager<IdentityRole> roleManager)
        {
            _roleMgr = roleManager;
        }
        public async Task<IdentityResult> AddRole(string name)
        {
            return await _roleMgr.CreateAsync(new IdentityRole(name));
        }

        public async Task<IdentityResult> RemoveRole(string roleName)
        {
            var roleToRemove = await _roleMgr.FindByNameAsync(roleName);
            return await _roleMgr.DeleteAsync(roleToRemove);
        }
    }
}
