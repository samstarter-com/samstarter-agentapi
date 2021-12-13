using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.DataAccess.Common2
{
    public interface IRepositoryReadOnly<T> where T : class
    {
        T GetById(object key);

        ValueTask<T> GetByIdAsync(object key);

        IQueryable<T> Query(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        IEnumerable<T> GetAllLocal();
    }
}