namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    
    public partial class LicenseRequestDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string HcLocation { get; set; }
        public System.Guid UniqueId { get; set; }
        public int LicenseRequestId { get; set; }
    
        public virtual LicenseRequest LicenseRequest { get; set; }
    }
}
