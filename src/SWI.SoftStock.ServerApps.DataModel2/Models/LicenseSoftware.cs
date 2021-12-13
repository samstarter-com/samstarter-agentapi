namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LicenseSoftware
    {
        public LicenseSoftware()
        {
            this.LicenseMachineSoftwares = new HashSet<LicenseMachineSoftware>();
        }
    
        [Key]
        public int Id { get; set; }
        public int LicenseId { get; set; }
        public int SoftwareId { get; set; }
    
        public virtual License License { get; set; }
        public virtual Software Software { get; set; }
        public virtual ICollection<LicenseMachineSoftware> LicenseMachineSoftwares { get; set; }
    }
}
