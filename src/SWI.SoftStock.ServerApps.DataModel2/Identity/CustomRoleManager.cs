using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SWI.SoftStock.ServerApps.DataModel2.Identity.Models;
using System.Collections.Generic;
using System.Linq;

namespace SWI.SoftStock.ServerApps.DataModel2.Identity
{
    public class CustomRoleManager : RoleManager<CustomRole>
    {
        private readonly CustomRoleStore store;
      
        public CustomRoleManager(IRoleStore<CustomRole> store, 
            IEnumerable<IRoleValidator<CustomRole>> roleValidators, 
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            ILogger<RoleManager<CustomRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
            this.store = store as CustomRoleStore;
        }

        public override IQueryable<CustomRole> Roles => (store.Context as MainDbContext)?.Roles;
    }
}

