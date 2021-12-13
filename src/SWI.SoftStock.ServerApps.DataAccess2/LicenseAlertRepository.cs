using System.Linq;
using Microsoft.EntityFrameworkCore;
using SWI.SoftStock.ServerApps.DataAccess.Common2;
using SWI.SoftStock.ServerApps.DataModel2;

namespace SWI.SoftStock.ServerApps.DataAccess2
{
    public class LicenseAlertRepository : DbContextRepository<LicenseAlert>
    {
        public LicenseAlertRepository(DbContext context)
            : base(context)
        {
        }

        public override void Delete(LicenseAlert entity)
        {
            var licenseAlertUserRepository = new DbContextRepository<LicenseAlertUser>(Context);
            licenseAlertUserRepository.DeleteRange(entity.Assignees.ToArray());
            base.Delete(entity);
        }

        public override void DeleteRange(LicenseAlert[] entities)
        {
            Context.Set<LicenseAlertUser>().RemoveRange(entities.SelectMany(la => la.Assignees));
            base.DeleteRange(entities);
        }
    }
}