namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    
    public partial class MachineSoftwareHistory
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int SoftwareId { get; set; }
        public SoftwareStatus Status { get; set; }
        public string InstallDate { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual Machine Machine { get; set; }
        public virtual Software Software { get; set; }
    }
}
