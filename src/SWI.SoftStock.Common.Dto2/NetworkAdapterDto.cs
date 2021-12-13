using System;
using System.Runtime.Serialization;

namespace SWI.SoftStock.Common.Dto2
{
    [DataContract]
    public class NetworkAdapterDto : IEquatable<NetworkAdapterDto>
    {
        [DataMember(Order = 0)]
        public string Caption { get; set; }

        [DataMember(Order = 1)]
        public string MacAdress { get; set; }

        #region IEquatable<NetworkAdapterDto> Members

        public bool Equals(NetworkAdapterDto other)
        {
            if (other == null)
                return false;

            if ((Caption != other.Caption)
                || (MacAdress != other.MacAdress))
                return false;
            return true;
        }

        #endregion

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var currentObj = obj as NetworkAdapterDto;
            return currentObj != null && Equals(currentObj);
        }
    }
}