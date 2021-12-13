namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System.Collections.Generic;

    public partial class Software
    {
        public Software()
        {
            this.MachineSoftwares = new HashSet<MachineSoftware>();
            this.LicenseSoftwares = new HashSet<LicenseSoftware>();
            this.MachineSoftwareHistories = new HashSet<MachineSoftwareHistory>();
            this.LicenseRequests = new HashSet<LicenseRequest>();
            this.Observables = new HashSet<Observable>();
            this.SoftwareCurrentLinkedStructureUnitReadOnlys = new HashSet<SoftwareCurrentLinkedStructureUnitReadOnly>();
            this.MachineSoftwareLicenseReadOnlys = new HashSet<MachineSoftwareLicenseReadOnly>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public int? PublisherId { get; set; }
        public string SystemComponent { get; set; }
        public string ReleaseType { get; set; }
        public string WindowsInstaller { get; set; }
        public System.Guid UniqueId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual ICollection<MachineSoftware> MachineSoftwares { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<LicenseSoftware> LicenseSoftwares { get; set; }
        public virtual ICollection<MachineSoftwareHistory> MachineSoftwareHistories { get; set; }
        public virtual ICollection<LicenseRequest> LicenseRequests { get; set; }
        public virtual ICollection<Observable> Observables { get; set; }
        public virtual ICollection<MachineSoftwareLicenseReadOnly> MachineSoftwareLicenseReadOnlys { get; set; }

        public virtual ICollection<SoftwareCurrentLinkedStructureUnitReadOnly> SoftwareCurrentLinkedStructureUnitReadOnlys { get; set; }
    }
}
