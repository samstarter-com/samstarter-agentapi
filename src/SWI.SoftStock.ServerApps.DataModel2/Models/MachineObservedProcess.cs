namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    
    public partial class MachineObservedProcess
    {
        public MachineObservedProcess()
        {
            this.Processes = new HashSet<Process>();
        }
    
        public int Id { get; set; }
        public int ObservableId { get; set; }
        public System.Guid UniqueId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int MachineId { get; set; }
    
        public virtual Machine Machine { get; set; }
        public virtual Observable Observable { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
    }
}
