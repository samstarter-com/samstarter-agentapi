namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class License
    {
        public License()
        {
            this.LicenseSoftwares = new HashSet<LicenseSoftware>();
            this.Documents = new HashSet<Document>();
            this.LicenseAlerts = new HashSet<LicenseAlert>();
            this.MachineSoftwareLicenseReadOnlys = new HashSet<MachineSoftwareLicenseReadOnly>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int LicenseTypeId { get; set; }
        public System.DateTime BeginDate { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public string Comments { get; set; }
        public int Count { get; set; }
        public int StructureUnitId { get; set; }
        public System.Guid UniqueId { get; set; }
        public int? LicenseRequest_Id { get; set; }

        public virtual LicenseType LicenseType { get; set; }
        public virtual ICollection<LicenseSoftware> LicenseSoftwares { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<LicenseAlert> LicenseAlerts { get; set; }
        public virtual StructureUnit StructureUnit { get; set; }
        public virtual LicenseRequest LicenseRequest { get; set; }
        public virtual ICollection<MachineSoftwareLicenseReadOnly> MachineSoftwareLicenseReadOnlys { get; set; }
    }
}
