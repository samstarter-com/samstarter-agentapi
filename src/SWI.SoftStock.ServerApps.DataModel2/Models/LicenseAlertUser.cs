namespace SWI.SoftStock.ServerApps.DataModel2
{
    
    public partial class LicenseAlertUser
    {
        public int Id { get; set; }
        public int LicenseAlertId { get; set; }
        public System.Guid UserUserId { get; set; }
    
        public virtual LicenseAlert LicenseAlert { get; set; }
        public virtual User User { get; set; }
    }
}
