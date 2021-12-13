using SWI.SoftStock.ServerApps.DataModel2.Identity.Models;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SWI.SoftStock.ServerApps.DataModel2.Identity
{
    public class CustomRoleStore : RoleStore<CustomRole, DbContext, Guid>
    {
        public CustomRoleStore(DbContext context) : base(context)
        {
        }
    }
}
