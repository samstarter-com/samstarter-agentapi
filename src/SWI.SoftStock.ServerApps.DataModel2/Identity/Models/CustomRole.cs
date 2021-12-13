using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SWI.SoftStock.ServerApps.DataModel2.Identity.Models
{
    public class CustomRole : IdentityRole<Guid>
    {
        public CustomRole() : base()
        {
            this.Id = Guid.NewGuid();
            this.StructureUnitRoles = new HashSet<StructureUnitUserRole>();
        }

        public virtual ICollection<StructureUnitUserRole> StructureUnitRoles { get; set; }
    }
}
