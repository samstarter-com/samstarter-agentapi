namespace SWI.SoftStock.ServerApps.DataModel2
{
    public class SoftwareCurrentLinkedStructureUnitReadOnly
    {
        public int SoftwareId { get; set; }
        public int CurrentLinkedStructureUnitId { get; set; }

        public int SoftwaresTotalCount { get; set; }
        public int SoftwaresIsActiveCount { get; set; }
        public int SoftwaresIsExpiredLicenseCount { get; set; }

        public int SoftwaresUnlicensedCount { get; set; }

        public virtual Software Software { get; set; }

    }
}