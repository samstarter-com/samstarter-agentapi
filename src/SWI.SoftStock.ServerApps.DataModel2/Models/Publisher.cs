namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Publisher
    {
        public Publisher()
        {
            this.Softwares = new HashSet<Software>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Software> Softwares { get; set; }
    }
}
