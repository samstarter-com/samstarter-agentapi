using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.DataAccess.Common2
{
    public class UnitOfWorkBase : IDisposable
    {
        protected readonly DbContext context;

        private bool disposed;

        public UnitOfWorkBase(DbContext context)
        {
            this.context = context;
        }

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        ///     Disposes off the managed and unmanaged resources used.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (disposed)
            {
                return;
            }

            context.Dispose();

            disposed = true;
        }

        public void Save()
        {
          
                var entities = from e in context.ChangeTracker.Entries()
                    where e.State == EntityState.Added
                          || e.State == EntityState.Modified
                    select e.Entity;
                foreach (var entity in entities)
                {
                    var validationContext = new ValidationContext(entity);
                    Validator.ValidateObject(entity, validationContext);
                }

                context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            var entities = from e in context.ChangeTracker.Entries()
                where e.State == EntityState.Added
                      || e.State == EntityState.Modified
                select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }
            return context.SaveChangesAsync();
        }
    }
}
