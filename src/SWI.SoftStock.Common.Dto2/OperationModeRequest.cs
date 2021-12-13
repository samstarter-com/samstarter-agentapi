using System;

namespace SWI.SoftStock.Common.Dto2
{
    public class OperationModeRequest : Request
    {
        public OperationModeDto OperationMode { get; set; }
        public Guid MachineUniqueId { get; set; }
        public Guid OperationSystemUniqueId { get; set; }
    }
}