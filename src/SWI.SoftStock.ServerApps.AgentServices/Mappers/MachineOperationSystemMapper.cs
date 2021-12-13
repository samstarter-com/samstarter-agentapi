using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.DataModel2;

namespace SWI.SoftStock.ServerApps.AgentServices.Mappers
{
    public static class MachineOperationSystemMapper
    {
        public static MachineOperationSystem ToModel(OperationModeDto operationMode, Machine machine, OperationSystem operationSystem)
        {
            var result = new MachineOperationSystem();
            result.BootMode = operationMode.BootMode;
            result.EnvironmentVariables = operationMode.EnvironmentVariables;
            result.LogicalDrives = operationMode.LogicalDrives;
            result.Machine = machine;
            result.OperationSystem = operationSystem;
            result.Secure = operationMode.Secure;
            result.SerialNumber = operationMode.SerialNumber;
            result.SystemDirectory = operationMode.SystemDirectory;
            return result;
        }
    }
}