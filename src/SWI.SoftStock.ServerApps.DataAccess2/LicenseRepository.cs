using Microsoft.EntityFrameworkCore;
using SWI.SoftStock.ServerApps.DataAccess.Common2;
using SWI.SoftStock.ServerApps.DataModel2;
using System.Linq;

namespace SWI.SoftStock.ServerApps.DataAccess2
{
    public class LicenseRepository : DbContextRepository<License>
    {
        public LicenseRepository(DbContext context)
            : base(context)
        {
        }

        public override void Delete(License entity)
        {
            Context.Set<LicenseSoftware>().RemoveRange(entity.LicenseSoftwares);
            Context.Set<Document>().RemoveRange(entity.Documents);
            var licenseAlertRepository = new LicenseAlertRepository(Context);
            licenseAlertRepository.DeleteRange(entity.LicenseAlerts.ToArray());
            base.Delete(entity);
        }
    }
}