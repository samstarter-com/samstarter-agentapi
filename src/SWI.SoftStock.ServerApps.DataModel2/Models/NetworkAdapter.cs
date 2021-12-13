namespace SWI.SoftStock.ServerApps.DataModel2
{

    public partial class NetworkAdapter
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public string Caption { get; set; }
        public string MacAdress { get; set; }
    
        public virtual Machine Machine { get; set; }

      
    }
}
