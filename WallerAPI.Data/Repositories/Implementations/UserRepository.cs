using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data.Repositories.Interfaces;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userMgr;

        public UserRepository(UserManager<User> userManager)
        {
            _userMgr = userManager;
        }

        public void Add(User entity)
        {
            _userMgr.CreateAsync(entity);
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return _userMgr.Users.Where(predicate);
        }

        public async Task<User> Get(string id)
        {
            return  await _userMgr.FindByIdAsync(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userMgr.Users;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userMgr.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> Register(User user, string password)
        {
            return await _userMgr.CreateAsync(user, "mYP@55word");
            
        }

        public void Remove(User user)
        {
            _userMgr.DeleteAsync(user);
        }

        public async Task<IdentityResult> AddUserToRole(User user, string role)
        {
            return await _userMgr.AddToRoleAsync(user, role);
        }
        
        public async Task<IdentityResult> RemoveUserFromRole(User user, string role)
        {
            return await _userMgr.RemoveFromRoleAsync(user, role);
        }

        public int RowCount()
        {
            return _userMgr.Users.Count();
        }
    }
}
