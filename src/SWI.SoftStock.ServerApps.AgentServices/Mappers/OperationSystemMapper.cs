using System;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.DataModel2;

namespace SWI.SoftStock.ServerApps.AgentServices.Mappers
{
    public static class OperationSystemMapper
    {
        public static OperationSystem ToModel(OperationSystemDto operationSystem)
        {
            var result = new OperationSystem();
            result.Architecture = operationSystem.Architecture;
            result.BuildNumber = operationSystem.BuildNumber;
            result.MaxNumberOfProcesses = operationSystem.MaxNumberOfProcesses;
            result.MaxProcessMemorySize = Convert.ToInt64(operationSystem.MaxProcessMemorySize);
            result.Name = operationSystem.Name;
            result.Version = operationSystem.Version;
            return result;
        }
    }
}