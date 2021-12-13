namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System.Collections.Generic;
    
    public partial class LicenseAlert
    {
        public LicenseAlert()
        {
            this.Assignees = new HashSet<LicenseAlertUser>();
        }
    
        public int Id { get; set; }
        public int LicenseId { get; set; }
        public System.DateTime AlertDate { get; set; }
        public string Text { get; set; }
        public System.Guid UniqueId { get; set; }
    
        public virtual License License { get; set; }
        public virtual ICollection<LicenseAlertUser> Assignees { get; set; }
    }
}
