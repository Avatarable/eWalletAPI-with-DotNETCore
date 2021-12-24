using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data.Repositories.Interfaces;

namespace WallerAPI.Data.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleMgr;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleMgr = roleManager;
        }
        public async Task AddRole(string name)
        {
            await _roleMgr.CreateAsync(new IdentityRole(name));
        }

        public async Task RemoveRole(string role)
        {
            var roleToRemove = await _roleMgr.FindByNameAsync(role);
            await _roleMgr.DeleteAsync(roleToRemove);
        }
    }
}
