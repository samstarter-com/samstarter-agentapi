using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SWI.SoftStock.ServerApps.DataModel2;
using SWI.SoftStock.ServerApps.DataModel2.Identity.Models;
using System;

namespace SWI.SoftStock.ServerApps.DataModel.Identity
{
    public class CustomUserStore : UserStore<User, CustomRole, DbContext, Guid>
    {
        public CustomUserStore(DbContext context) : base(context)
        {
        }
    } 
}
