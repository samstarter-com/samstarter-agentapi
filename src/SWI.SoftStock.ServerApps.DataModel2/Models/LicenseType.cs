namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LicenseType
    {
        public LicenseType()
        {
            this.Licenses = new HashSet<License>();
        }
    
        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<License> Licenses { get; set; }
    }
}
