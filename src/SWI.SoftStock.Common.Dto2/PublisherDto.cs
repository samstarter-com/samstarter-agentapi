using System;

namespace SWI.SoftStock.Common.Dto2
{
    public class PublisherDto : IEquatable<PublisherDto>
    {
        public string Name { get; set; }

        #region IEquatable<PublisherDto> Members

        public bool Equals(PublisherDto other)
        {
            if (other == null)
                return false;

            if (Name != other.Name)
                return false;

            return true;
        }

        #endregion

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var currentObj = obj as PublisherDto;
            return currentObj != null && Equals(currentObj);
        }

        public override string ToString()
        {
            return String.Format("Name:{0}", Name);
        }
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                if (Name != null)
                {
                    hash = hash * 23 + Name.GetHashCode();
                }
                return hash;
            }
        }
    }
}