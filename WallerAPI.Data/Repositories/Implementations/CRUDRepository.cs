using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WallerAPI.Data.Repositories.Interfaces;

namespace WallerAPI.Data.Repositories.Implementations
{
    public class CRUDRepository<T> : ICRUDRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public CRUDRepository(DbContext context)
        {
            Context = context;
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public async Task<T> Get(string id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public int RowCount()
        {
            return Context.Set<T>().Count();
        }
    }
}
