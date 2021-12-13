namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public Account()
        {
            this.StructureUnits = new HashSet<StructureUnit>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int MachineCount { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
        public int DefaultAgentInterval { get; set; }

        public virtual ICollection<StructureUnit> StructureUnits { get; set; }
    }
}
