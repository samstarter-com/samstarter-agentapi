namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Linq;

	public static class EntityExtensions
	{
		public static int TotalUsedLicenseCount(this License license, int[] exceptMachineIds = null)
		{
			if (exceptMachineIds == null || exceptMachineIds.Length == 0)
			{
				return license.LicenseSoftwares.SelectMany(ls => ls.LicenseMachineSoftwares).Where(lms => !lms.IsDeleted).
					Select(lms => lms.MachineSoftware.MachineId).Distinct().Count();
			}
			else
			{
				return license.LicenseSoftwares.SelectMany(ls => ls.LicenseMachineSoftwares).Where(lms => !lms.IsDeleted).
					Select(lms => lms.MachineSoftware.MachineId).Where(mid => !exceptMachineIds.Contains(mid)).Distinct().Count();
			}
		}
	}

    public partial class Machine : IUniqueId, IAuditable
    {
       
    }

    public partial class MachineSoftware : IAuditable
    {
    }

    public partial class MachineSoftwareHistory : IAuditable
    {
    }

    public partial class StructureUnit : IUniqueId, IAuditable, IEquatable<StructureUnit>
    {
        #region IEquatable<StructureUnit> Members

        public bool Equals(StructureUnit other)
        {
            if (other == null)
            {
                return false;
            }

            if ((UniqueId != other.UniqueId)
                || (Name != other.Name)
                || (ShortName != other.ShortName)
                )
            {
                return false;
            }

            return true;
        }

        #endregion
    }

    public partial class Software : IAuditable, IUniqueId
    {
        public override string ToString()
        {
            return String.Format("Name:{0} Version:{1} Publisher:{2}", Name, Version, Publisher);
        }
    }

    public partial class Publisher
    {
        public override string ToString()
        {
            return String.Format("Id:{0} Name:{1}", Id, Name);
        }
    }

    public partial class OperationSystem : IUniqueId, IAuditable
    {
    }

    public partial class License : IUniqueId
    {
    }

    public partial class LicenseAlert : IUniqueId
    {
    }

    public partial class Document : IUniqueId
    {
    }

    public partial class LicenseRequestDocument : IUniqueId
    {
    }

    public partial class LicenseRequest : IUniqueId
    {
    }

    public partial class DomainUser : IEquatable<DomainUser>, IAuditable
    {
        #region IEquatable<DomainUser> Members

        public bool Equals(DomainUser other)
        {
            if (other == null)
            {
                return false;
            }

            if ((Name != other.Name)
                || (DomainName != other.DomainName)
                )
            {
                return false;
            }

            return true;
        }

        #endregion
    }

    public partial class LicenseAlert : IEquatable<LicenseAlert>
    {
        #region IEquatable<LicenseAlert> Members

        public bool Equals(LicenseAlert other)
        {
            if (other == null)
            {
                return false;
            }

            if ((AlertDate != other.AlertDate)
                || (Text != other.Text)
                || (!Assignees.SequenceEqual(other.Assignees))
                )
            {
                return false;
            }

            return true;
        }

        #endregion
    }

    public partial class LicenseAlertUser : IEquatable<LicenseAlertUser>
    {
        #region IEquatable<LicenseAlertUser> Members

        public bool Equals(LicenseAlertUser other)
        {
            if (other == null)
            {
                return false;
            }

            return UserUserId == other.UserUserId;
        }

        #endregion
    }

    public partial class Observable : IUniqueId, IAuditable
    {
    }

    public partial class MachineObservedProcess : IUniqueId
    {
    }

    public partial class Feedback : IAuditable
    {
    }
}