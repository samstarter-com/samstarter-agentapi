using System;

namespace SWI.SoftStock.Common.Dto2
{
    public class ProcessDto
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public ProcessStatus Status { get; set; }
        public Guid MachineObservableUniqueId { get; set; }
    }
}