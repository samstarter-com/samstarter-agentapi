using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SWI.SoftStock.Common.Dto2
{
    [DataContract]
    public class MachineDto : IEquatable<MachineDto>
    {
        [DataMember(Order = 0)]
        public Guid CompanyUniqueId { get; set; }

        [DataMember(Order = 1)]
        public Guid UniqueId { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public int MonitorCount { get; set; }

        [DataMember(Order = 4)]
        public bool MonitorsSameDisplayFormat { get; set; }

        [DataMember(Order = 5)]
        public int MouseButtons { get; set; }

        [DataMember(Order = 6)]
        public string ScreenOrientation { get; set; }

        [DataMember(Order = 7)]
        public int ProcessorCount { get; set; }

        [DataMember(Order = 8)]
        public double MemoryTotalCapacity { get; set; }

        [DataMember(Order = 9)]
        public ProcessorDto Processor { get; set; }

        [DataMember(Order = 10)]
        public OperationSystemDto OperationSystem { get; set; }

        [DataMember(Order = 11)]
        public IList<NetworkAdapterDto> NetworkAdapters { get; set; }

        #region IEquatable<MachineDto> Members

        public bool Equals(MachineDto other)
        {
            if (other == null)
                return false;

            if ((Name != other.Name)
                || (MonitorCount != other.MonitorCount)
                || (MonitorsSameDisplayFormat != other.MonitorsSameDisplayFormat)
                || (MouseButtons != other.MouseButtons)
                || (ScreenOrientation != other.ScreenOrientation)
                || (ProcessorCount != other.ProcessorCount)
                || (Math.Abs(MemoryTotalCapacity - other.MemoryTotalCapacity) > 0.01)
                || ((Processor != null) && (!Processor.Equals(other.Processor)))
                || ((Processor == null) && (other.Processor != null))
                || (!NetworkAdapters.SequenceEqual(other.NetworkAdapters))
                || (CompanyUniqueId != other.CompanyUniqueId)
                )
                return false;

            return true;
        }

        #endregion

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var currentObj = obj as MachineDto;
            return currentObj != null && Equals(currentObj);
        }
    }
}