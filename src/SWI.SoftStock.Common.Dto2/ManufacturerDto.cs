using System;
using System.Runtime.Serialization;

namespace SWI.SoftStock.Common.Dto2
{
    [DataContract]
    public class ManufacturerDto : IEquatable<ManufacturerDto>
    {
        [DataMember(Order = 0)]
        public string Name { get; set; }

        #region IEquatable<ManufacturerDto> Members

        public bool Equals(ManufacturerDto other)
        {
            if (other == null)
                return false;
            return Name == other.Name;
        }

        #endregion

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var currentObj = obj as ManufacturerDto;
            return currentObj != null && Equals(currentObj);
        }
    }
}