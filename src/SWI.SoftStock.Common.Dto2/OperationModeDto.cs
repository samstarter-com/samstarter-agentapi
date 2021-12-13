using System;

namespace SWI.SoftStock.Common.Dto2
{
    public class OperationModeDto : IEquatable<OperationModeDto>
    {
        public string BootMode { get; set; }
        public string EnvironmentVariables { get; set; }
        public string LogicalDrives { get; set; }
        public bool Secure { get; set; }
        public string SerialNumber { get; set; }
        public string SystemDirectory { get; set; }

        #region IEquatable<OperationModeDto> Members

        public bool Equals(OperationModeDto other)
        {
            if (other == null)
                return false;

            if ((BootMode != other.BootMode)
                || (Secure != other.Secure)
                || (SystemDirectory != other.SystemDirectory)
                || (EnvironmentVariables != other.EnvironmentVariables)
                || (LogicalDrives != other.LogicalDrives)
                || (SerialNumber != other.SerialNumber)
                )
                return false;
            return true;
        }

        #endregion
    }
}