namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            this.Processors = new HashSet<Processor>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Processor> Processors { get; set; }
    }
}
