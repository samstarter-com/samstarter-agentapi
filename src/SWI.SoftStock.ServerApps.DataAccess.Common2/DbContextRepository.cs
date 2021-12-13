using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.DataAccess.Common2
{
    public class DbContextRepository<T> : GenericRepository<T>
        where T : class
    {
        public DbContextRepository(DbContext context)
        {
            Context = context ?? throw new ArgumentException("context");
            DbSet = Context.Set<T>();
        }

        public DbContext Context { get; set; }

        public DbSet<T> DbSet { get; set; }

        public override void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            DbSet.Add(entity);
        }

        public override void AddRange(T[] entities)
        {
            if (entities.Any(entity => entity == null))
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            DbSet.AddRange(entities);
        }

        public override void DeleteRange(T[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    throw new ArgumentException("Cannot delete a null entity.");
                }
                DbSet.Attach(entity);
            }
            DbSet.RemoveRange(entities);
        }

        public override void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot delete a null entity.");
            }
            DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public override void Update(T entity, object id)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot update a null entity.");
            }

            var entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                T attachedEntity = DbSet.Find(id); // Need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = Context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
        }

        public override T GetById(object key)
        {
            return DbSet.Find(key);
        }

        public override ValueTask<T> GetByIdAsync(object key)
        {
            return DbSet.FindAsync(key);
        }

        public override IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public override IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public override IEnumerable<T> GetAllLocal()
        {
            return DbSet.Local;
        }

        public override int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}