using System;

namespace SWI.SoftStock.Common.Dto2
{
    public class OperationSystemRequest : Request
    {
        public OperationSystemDto OperationSystem { get; set; }

        public Guid MachineUniqueId { get; set; }
    }
}