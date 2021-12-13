using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.DataModel2;
using SoftwareStatus = SWI.SoftStock.ServerApps.DataModel2.SoftwareStatus;

namespace SWI.SoftStock.ServerApps.AgentServices.Mappers
{
    public static class SoftwareMapper
    {
        public static SoftwareResponse ToResponse(Software software)
        {
            var result = new SoftwareResponse();
            return result;
        }

        public static Software ToModel(SoftwareDto software)
        {
            var result = new Software();
            result.Name = software.Name;
            result.Version = software.Version;
            result.ReleaseType = software.ReleaseType;
            result.SystemComponent = software.SystemComponent;
            result.WindowsInstaller = software.WindowsInstaller;
            result.Publisher = software.Publisher != null ? new Publisher {Name = software.Publisher.Name} : null;
            return result;
        }

        public static SoftwareStatus ToModelStatus(SWI.SoftStock.Common.Dto2.SoftwareStatus status)
        {
            switch (status)
            {
                case SWI.SoftStock.Common.Dto2.SoftwareStatus.Same:
                    return SoftwareStatus.Same;
                case SWI.SoftStock.Common.Dto2.SoftwareStatus.Removed:
                    return SoftwareStatus.Removed;
                case SWI.SoftStock.Common.Dto2.SoftwareStatus.Added:
                    return SoftwareStatus.Added;
            }
            return SoftwareStatus.Same;
        }
    }
}