using System;
using System.Runtime.Serialization;

namespace SWI.SoftStock.Common.Dto2
{
    [DataContract]
    public class OperationSystemDto : IEquatable<OperationSystemDto>
    {
        [DataMember(Order = 0)]
        public string Name { get; set; }

        [DataMember(Order = 1)]
        public string Version { get; set; }

        [DataMember(Order = 2)]
        public uint MaxNumberOfProcesses { get; set; }

        [DataMember(Order = 3)]
        public ulong MaxProcessMemorySize { get; set; }

        [DataMember(Order = 4)]
        public string Architecture { get; set; }

        [DataMember(Order = 5)]
        public string BuildNumber { get; set; }

        [DataMember(Order = 6)]
        public Guid UniqueId { get; set; }

        #region IEquatable<OperationSystemDto> Members

        public bool Equals(OperationSystemDto other)
        {
            if (other == null)
                return false;

            if ((Name != other.Name)
                || (Version != other.Version)
                || (MaxNumberOfProcesses != other.MaxNumberOfProcesses)
                || (MaxProcessMemorySize != other.MaxProcessMemorySize)
                || (Architecture != other.Architecture)
                || (BuildNumber != other.BuildNumber)
                )
                return false;
            return true;
        }

        #endregion

        public override string ToString()
        {
            return
                String.Format(
                    "Name:{0} Version:{1} MaxNumberOfProcesses:{2} MaxProcessMemorySize:{3} Architecture:{4} BuildNumber:{5}",
                    Name, Version, MaxNumberOfProcesses, MaxProcessMemorySize, Architecture, BuildNumber);
        }
    }
}