namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class LicenseMachineSoftware
    {
        [Key]
        public int Id { get; set; }
        public int LicenseSoftwareId { get; set; }
        public int MachineSoftwareId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual LicenseSoftware LicenseSoftware { get; set; }
        public virtual MachineSoftware MachineSoftware { get; set; }
    }
}
