using Microsoft.EntityFrameworkCore;
using SWI.SoftStock.ServerApps.DataAccess.Common2;
using SWI.SoftStock.ServerApps.DataModel2;
using System.Linq;

namespace SWI.SoftStock.ServerApps.DataAccess2
{
    public class UserRepository : DbContextRepository<User>
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }

        public override void Delete(User entity)
        {
            var todeleteUserRoles = Context.Set<StructureUnitUserRole>().Single(
                suur => suur.UserUserId == entity.Id && suur.Role.Name == "User");
            Context.Set<StructureUnitUserRole>().Remove(todeleteUserRoles);

            var toDeleteMachineHistory = Context.Set<MachineUser>().Where(mu => mu.UserUserId == entity.Id);
            Context.Set<MachineUser>().RemoveRange(toDeleteMachineHistory);

            base.Delete(entity);
        }
    }

    public class StructureUnitRepository : DbContextRepository<StructureUnit>
    {
        public StructureUnitRepository(DbContext context)
            : base(context)
        {
        }

        public override void Delete(StructureUnit entity)
        {
            var todeleteUsereRoles = Context.Set<StructureUnitUserRole>().Where(
                suur =>
                suur.StructureUnitId == entity.Id && (suur.Role.Name == "Admin" || suur.Role.Name == "Manager"));
            Context.Set<StructureUnitUserRole>().RemoveRange(todeleteUsereRoles);

            base.Delete(entity);
        }
    }
}