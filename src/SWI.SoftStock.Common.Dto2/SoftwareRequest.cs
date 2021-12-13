using System;

namespace SWI.SoftStock.Common.Dto2
{
    public class SoftwareRequest : Request
    {
        public SoftwareStatusDto[] Softwares { get; set; }
        public Guid MachineUniqueId { get; set; }
    }
}