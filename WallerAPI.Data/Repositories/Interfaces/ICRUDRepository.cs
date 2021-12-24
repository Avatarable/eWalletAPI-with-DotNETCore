using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WallerAPI.Data.Repositories.Interfaces
{
    public interface ICRUDRepository<T> where T : class
    {
        Task<T> Get(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Remove(T entity);

        int RowCount();
    }
}
