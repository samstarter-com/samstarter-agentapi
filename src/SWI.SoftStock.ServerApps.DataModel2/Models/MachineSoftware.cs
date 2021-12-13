namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class MachineSoftware
    {
        public MachineSoftware()
        {
            this.LicenseMachineSoftwares = new HashSet<LicenseMachineSoftware>();
        }

        [Key]
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int SoftwareId { get; set; }
        public string InstallDate { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }

        public virtual Software Software { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual ICollection<LicenseMachineSoftware> LicenseMachineSoftwares { get; set; }

        public virtual MachineSoftwareLicenseReadOnly MachineSoftwareLicenseReadOnly { get; set; }
    }
}
