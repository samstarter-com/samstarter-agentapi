namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    
    public partial class MachineUser
    {
        public int Id { get; set; }
        public System.DateTime LinkDateTime { get; set; }
        public int MachineId { get; set; }
        public System.Guid UserUserId { get; set; }
    
        public virtual Machine Machine { get; set; }
        public virtual User User { get; set; }
    }
}
