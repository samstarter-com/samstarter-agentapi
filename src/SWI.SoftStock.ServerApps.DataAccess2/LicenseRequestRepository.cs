using Microsoft.EntityFrameworkCore;
using SWI.SoftStock.ServerApps.DataAccess.Common2;
using SWI.SoftStock.ServerApps.DataModel2;
using System;
using System.Linq;

namespace SWI.SoftStock.ServerApps.DataAccess2
{
    public class LicenseRequestRepository : DbContextRepository<LicenseRequest>
    {
        public LicenseRequestRepository(DbContext context)
            : base(context)
        {
        }

        public override void Delete(LicenseRequest entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot delete a null entity.");
            }

            Context.Set<LicenseRequestHistory>().RemoveRange(entity.LicenseRequestHistories);
            base.Delete(entity);
        }

        public override void DeleteRange(LicenseRequest[] entities)
        {
            Context.Set<LicenseRequestHistory>().RemoveRange(entities.SelectMany(e=>e.LicenseRequestHistories));
            base.DeleteRange(entities);
        }

        public override void Add(LicenseRequest entity)
        {
            base.Add(entity);
            Context.Set<LicenseRequestHistory>().Add(new LicenseRequestHistory() { LicenseRequest = entity, Status = entity.CurrentStatus, StatusDateTime = entity.CurrentStatusDateTime });
        }

        public override void AddRange(LicenseRequest[] entities)
        {
            base.AddRange(entities);
            Context.Set<LicenseRequestHistory>().AddRange(entities.Select(entity => new LicenseRequestHistory() { LicenseRequest = entity, Status = entity.CurrentStatus, StatusDateTime = entity.CurrentStatusDateTime }));
        }

        public override void Update(LicenseRequest entity, object id)
        {
            base.Update(entity, id);
            Context.Set<LicenseRequestHistory>().Add(new LicenseRequestHistory() { LicenseRequest = entity, Status = entity.CurrentStatus, StatusDateTime = entity.CurrentStatusDateTime });
        }
    }
}