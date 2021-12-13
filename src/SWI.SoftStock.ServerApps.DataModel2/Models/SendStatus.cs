namespace SWI.SoftStock.ServerApps.DataModel2
{
    
    public enum SendStatus : int
    {
        None = 0,
        Sending = 1,
        Success = 2,
        Error = 3,
        MaxSendingCountError = 4
    }
}
