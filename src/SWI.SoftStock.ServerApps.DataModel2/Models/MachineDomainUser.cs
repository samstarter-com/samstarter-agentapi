namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    
    public partial class MachineDomainUser
    {
        public int Id { get; set; }
        public System.DateTime FirstDateTime { get; set; }
        public System.DateTime LastDateTime { get; set; }

        public int DomainUser_Id { get; set; }
        public int Machine_Id { get; set; }

        public virtual Machine Machine { get; set; }
        public virtual DomainUser DomainUser { get; set; }
    }
}
