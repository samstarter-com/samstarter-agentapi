namespace SWI.SoftStock.ServerApps.DataModel2
{
    public interface IAuditable
    {
        System.DateTime CreatedOn { get; set; }
        System.DateTime ModifiedOn { get; set; }
    }
}