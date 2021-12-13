using ProcessStatus = SWI.SoftStock.ServerApps.DataModel2.ProcessStatus;

namespace SWI.SoftStock.ServerApps.AgentServices.Mappers
{
    public class ProcessMapper
    {
        public static ProcessStatus ToModelStatus(SWI.SoftStock.Common.Dto2.ProcessStatus status)
        {
            switch (status)
            {
                case SWI.SoftStock.Common.Dto2.ProcessStatus.Started:
                    return ProcessStatus.Started;
                case SWI.SoftStock.Common.Dto2.ProcessStatus.Stoped:
                    return ProcessStatus.Stoped;
            }
            return ProcessStatus.Started;
        }
    }
}