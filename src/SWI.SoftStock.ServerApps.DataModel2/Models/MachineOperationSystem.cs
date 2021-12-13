namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System.ComponentModel.DataAnnotations;

    public partial class MachineOperationSystem
    {
        [Key]
        public int Id { get; set; }
        public string BootMode { get; set; }
        public string EnvironmentVariables { get; set; }
        public string LogicalDrives { get; set; }
        public bool Secure { get; set; }
        public string SerialNumber { get; set; }
        public string SystemDirectory { get; set; }

        //[ForeignKey(nameof(Machine))]
        public int MachineId { get; set; }

        //[ForeignKey(nameof(OperationSystem))]
        public int OperationSystemId { get; set; }

        public virtual OperationSystem OperationSystem { get; set; }
        public virtual Machine Machine { get; set; }
    }
}
