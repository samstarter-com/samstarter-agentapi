using System;

namespace SWI.SoftStock.Common.Dto2
{
    public  class  UserRequest:Request
    {
        public UserDto User { get; set; }
        public Guid MachineUniqueId { get; set; }
    }
}