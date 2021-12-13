namespace SWI.SoftStock.ServerApps.DataModel2
{
    
    public partial class Feedback
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public string UserIp { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    }
}
