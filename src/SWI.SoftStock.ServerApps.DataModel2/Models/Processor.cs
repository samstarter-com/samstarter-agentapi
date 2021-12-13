namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Processor
    {
        public Processor()
        {
            this.Machines = new HashSet<Machine>();
        }
        [Key]
        public int Id { get; set; }
        public string ProcessorId { get; set; }
        public string DeviceID { get; set; }
        public string SocketDesignation { get; set; }
        public bool Is64BitProcess { get; set; }
        public int? ManufacturerId { get; set; }

        public virtual ICollection<Machine> Machines { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
