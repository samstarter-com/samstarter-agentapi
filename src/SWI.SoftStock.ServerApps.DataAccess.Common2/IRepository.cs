using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.DataAccess.Common2
{
    public interface IRepository<T> : IRepositoryReadOnly<T> where T : class
    {
        void DeleteRange(T[] entities);

        void Delete(T entity);

        void Add(T entity);

        void AddRange(T[] entities);

        void Update(T entity, object id);

        int SaveChanges();
    }
}