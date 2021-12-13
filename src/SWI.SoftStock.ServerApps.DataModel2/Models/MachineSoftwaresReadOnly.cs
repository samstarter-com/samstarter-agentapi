using System.ComponentModel.DataAnnotations;

namespace SWI.SoftStock.ServerApps.DataModel2
{
    public class MachineSoftwaresReadOnly
    {
        [Key] 
        public int MachineId { get; set; }
        public int SoftwaresTotalCount { get; set; }
        public int SoftwaresIsActiveCount { get; set; }
        public int SoftwaresIsExpiredCount { get; set; }

        public int SoftwaresUnlicensedCount { get; set; }

        public virtual Machine Machine { get; set; }
    }
}