using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SWI.SoftStock.ServerApps.DataModel2.Identity.Models;
using System;

namespace SWI.SoftStock.ServerApps.DataModel2
{
    public class ApplicationDbContext : IdentityDbContext<User, CustomRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

    }
}
