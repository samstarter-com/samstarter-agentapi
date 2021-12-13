namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    
    public partial class OperationSystem
    {
        public OperationSystem()
        {
            this.MachineOperationSystems = new HashSet<MachineOperationSystem>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public long MaxNumberOfProcesses { get; set; }
        public long MaxProcessMemorySize { get; set; }
        public string Architecture { get; set; }
        public string BuildNumber { get; set; }
        public System.Guid UniqueId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual ICollection<MachineOperationSystem> MachineOperationSystems { get; set; }
    }
}
