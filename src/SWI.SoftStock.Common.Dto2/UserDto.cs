using System;

namespace SWI.SoftStock.Common.Dto2
{
    public class UserDto : IEquatable<UserDto>
    {
        public string UserDomainName { get; set; }
        public string UserName { get; set; }
        public bool IsEmpty { get; set; }

        #region IEquatable<UserDto> Members

        public bool Equals(UserDto other)
        {
            if (other == null)
                return false;

            return IsEmpty == other.IsEmpty && UserDomainName == other.UserDomainName && UserName == other.UserName;
        }

        #endregion
    }
}