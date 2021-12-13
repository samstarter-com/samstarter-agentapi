namespace SWI.SoftStock.ServerApps.DataModel2
{
    public class Process
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProcessStatus Status { get; set; }
        public System.DateTime DateTime { get; set; }
        public int MachineObservedProcessId { get; set; }
    
        public virtual MachineObservedProcess MachineObservedProcess { get; set; }
    }
}
