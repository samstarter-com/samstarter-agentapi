using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.DataAccess.Common2
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        #region IRepository<T> Members

        public abstract void DeleteRange(T[] entities);
        
        public abstract void Delete(T entity);

        public abstract void Add(T entity);

        public abstract void AddRange(T[] entities);

        public abstract void Update(T entity, object id);

        public abstract T GetById(object key);

        public abstract ValueTask<T> GetByIdAsync(object key);

        public abstract IQueryable<T> Query(Expression<Func<T, bool>> predicate);

        public abstract IQueryable<T> GetAll();

        public abstract IEnumerable<T> GetAllLocal();

        public abstract int SaveChanges();

        #endregion
    }
}