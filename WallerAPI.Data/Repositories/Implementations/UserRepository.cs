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

        public async Task Register(User user, string password, string role)
        {
            var res = await _userMgr.CreateAsync(user, "mYP@55word");
            if (res.Succeeded)
                await _userMgr.AddToRoleAsync(user, role);
        }

        public void Remove(User user)
        {
            _userMgr.DeleteAsync(user);
        }

        public void RemoveUserFromRole(User user, string role)
        {
            _userMgr.RemoveFromRoleAsync(user, role);
        }

        public int RowCount()
        {
            return _userMgr.Users.Count();
        }
    }
}
