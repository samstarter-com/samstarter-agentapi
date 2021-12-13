namespace SWI.SoftStock.ServerApps.DataModel2
{
   
    public enum LicenseRequestStatus : int
    {
        New = 0,
        SentToUser = 1,
        ViewedByUser = 2,
        SentToManager = 3,
        ViewedByManager = 4,
        Closed = 5,
        Archived = 6
    }
}