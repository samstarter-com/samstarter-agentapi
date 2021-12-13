using System.ComponentModel.DataAnnotations;

namespace SWI.SoftStock.ServerApps.DataModel2
{
    public class MachineSoftwareLicenseReadOnly
    {
        [Key] 
        public int MachineSoftwareId { get; set; }
        public int MachineId { get; set; }
        public int SoftwareId { get; set; }
        public int? LicenseId { get; set; }
        public int CurrentLinkedStructureUnitId { get; set; }
        public bool IsActive { get; set; }
        public virtual MachineSoftware MachineSoftware { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual Software Software { get; set; }

        public virtual License License { get; set; }
    }
}