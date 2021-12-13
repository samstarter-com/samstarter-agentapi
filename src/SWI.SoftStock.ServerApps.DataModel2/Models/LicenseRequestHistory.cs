using System.ComponentModel.DataAnnotations;

namespace SWI.SoftStock.ServerApps.DataModel2
{
    public partial class LicenseRequestHistory
    {
        [Key]
        public int Id { get; set; }
        public int LicenseRequestId { get; set; }
        public LicenseRequestStatus Status { get; set; }
        public System.DateTime StatusDateTime { get; set; }
    
        public virtual LicenseRequest LicenseRequest { get; set; }
    }
}
