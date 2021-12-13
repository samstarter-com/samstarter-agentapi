namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Observable
    {
        public Observable()
        {
            this.MachineObservedProcesses = new HashSet<MachineObservedProcess>();
        }
    
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public int SoftwareId { get; set; }
        public int CompanyId { get; set; }
        public System.Guid UniqueId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public System.Guid CreatedByUserId { get; set; }
    
        public virtual Software Software { get; set; }
        public virtual StructureUnit Company { get; set; }
        public virtual ICollection<MachineObservedProcess> MachineObservedProcesses { get; set; }
        public virtual User CreatedByUser { get; set; }
    }
}
