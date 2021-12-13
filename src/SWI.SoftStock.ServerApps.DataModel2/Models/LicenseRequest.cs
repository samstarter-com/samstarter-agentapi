using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWI.SoftStock.ServerApps.DataModel2
{
    public partial class LicenseRequest
    {
        public LicenseRequest()
        {
            this.LicenseRequestHistories = new HashSet<LicenseRequestHistory>();
            this.LicenseRequestDocuments = new HashSet<LicenseRequestDocument>();
            this.Licenses = new HashSet<License>();
        }

        [Key]
        public int Id { get; set; }
        public System.Guid UniqueId { get; set; }
        public int MachineId { get; set; }
        public int Software_Id { get; set; }
        public System.Guid UserUserId { get; set; }
        public string UserEmail { get; set; }
        public string RequestText { get; set; }
        public string UserAnswerText { get; set; }
        public System.Guid UserUserId1 { get; set; }
        public LicenseRequestStatus CurrentStatus { get; set; }
        public System.DateTime CurrentStatusDateTime { get; set; }

        public virtual Machine Machine { get; set; }
        public virtual User User { get; set; }
        public virtual Software Software { get; set; }
        public virtual User Manager { get; set; }
        public virtual ICollection<LicenseRequestHistory> LicenseRequestHistories { get; set; }
        public virtual ICollection<LicenseRequestDocument> LicenseRequestDocuments { get; set; }
        public virtual ICollection<License> Licenses { get; set; }
    }
}
