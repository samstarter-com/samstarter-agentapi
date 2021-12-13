using System;
using System.Runtime.Serialization;

namespace SWI.SoftStock.Common.Dto2
{
    [DataContract]
    public class ProcessorDto : IEquatable<ProcessorDto>
    {
        [DataMember(Order = 0)]
        public bool Is64BitProcess { get; set; }

        [DataMember(Order = 1)]
        public ManufacturerDto Manufacturer { get; set; }

        [DataMember(Order = 2)]
        public string ProcessorId { get; set; }

        [DataMember(Order = 3)]
        public string DeviceID { get; set; }

        [DataMember(Order = 4)]
        public string SocketDesignation { get; set; }

        #region IEquatable<ProcessorDto> Members

        public bool Equals(ProcessorDto other)
        {
            if (other == null)
                return false;

            if ((Is64BitProcess != other.Is64BitProcess)
                || (SocketDesignation != other.SocketDesignation)
                || (ProcessorId != other.ProcessorId)
                || (DeviceID != other.DeviceID)
                || ((Manufacturer != null) && (!Manufacturer.Equals(other.Manufacturer)))
                || ((Manufacturer == null) && (other.Manufacturer != null))
                )
                return false;
            return true;
        }

        #endregion

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var currentObj = obj as ProcessorDto;
            return currentObj != null && Equals(currentObj);
        }
    }
}