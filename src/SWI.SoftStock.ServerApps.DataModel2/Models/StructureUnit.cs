namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class StructureUnit
    {
        public StructureUnit()
        {
            this.ChildStructureUnits = new HashSet<StructureUnit>();
            this.Users = new HashSet<User>();
            this.CompanyMachines = new HashSet<Machine>();
            this.StructureUnitRoles = new HashSet<StructureUnitUserRole>();
            this.CurrentLinkedMachines = new HashSet<Machine>();
            this.Licenses = new HashSet<License>();
            this.Observables = new HashSet<Observable>();
        }
    
        [Key]
        public int Id { get; set; }
        public System.Guid UniqueId { get; set; }
        public UnitType UnitType { get; set; }
        public bool IsApproved { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int? StructureUnitId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int? AccountId { get; set; }
    
        public virtual ICollection<StructureUnit> ChildStructureUnits { get; set; }
        public virtual StructureUnit ParentStructureUnit { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Machine> CompanyMachines { get; set; }
        public virtual ICollection<StructureUnitUserRole> StructureUnitRoles { get; set; }
        public virtual ICollection<Machine> CurrentLinkedMachines { get; set; }
        public virtual ICollection<License> Licenses { get; set; }
        public virtual ICollection<Observable> Observables { get; set; }
        public virtual Account Account { get; set; }
    }
}
