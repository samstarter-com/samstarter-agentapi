using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.AgentServices.Mappers;
using SWI.SoftStock.ServerApps.DataAccess2;
using SWI.SoftStock.ServerApps.DataModel2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentServices
{
    public class OperationSystemService
    {
        private readonly ILogger<OperationSystemService> log;
        private readonly MainDbContextFactory dbFactory;

        public OperationSystemService(ILogger<OperationSystemService> log, MainDbContextFactory dbFactory)
        {
            this.log = log;
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        private OperationSystem GetExistingOperationSystem(IUnitOfWork unitOfWork, OperationSystem os)
        {
            Func<OperationSystem, bool> wherePredicate = item => item.Architecture == os.Architecture
                                                                 && item.BuildNumber == os.BuildNumber
                                                                 && item.MaxNumberOfProcesses == os.MaxNumberOfProcesses
                                                                 && item.MaxProcessMemorySize == os.MaxProcessMemorySize
                                                                 && item.Name == os.Name
                                                                 && item.Version == os.Version;


            var existingOperationSystem =
                unitOfWork.OperationSystemRepository.GetAllLocal().FirstOrDefault(wherePredicate) ??
                unitOfWork.OperationSystemRepository.GetAll().FirstOrDefault(wherePredicate);
            return existingOperationSystem;
        }

        #region IOperationSystemService Members

        public async Task<Response> AddOperationSystemAsync(OperationSystemRequest request)
        {
            var dbContext = dbFactory.Create();
            using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                Machine machine;
                try
                {
                    machine = await unitOfWork.MachineRepository.Query(c => c.UniqueId == request.MachineUniqueId).SingleAsync();
                }
                catch (Exception ex)
                {
                    log.LogError(0, ex, ex.Message);
                    return new Response { Code = 6, Message = ex.Message };
                }
                var operationSystem = OperationSystemMapper.ToModel(request.OperationSystem);
                var existingOperationSystem = GetExistingOperationSystem(unitOfWork, operationSystem);
                if (existingOperationSystem == null)
                {
                    unitOfWork.OperationSystemRepository.Add(operationSystem);

                    try
                    {
                        await unitOfWork.SaveAsync();
                    }
                    catch (Exception ex)
                    {
                        log.LogError(0, ex, ex.Message);
                        return new Response { Code = 7, Message = ex.Message };
                    }
                }

                return new Response
                {
                    Code = 0,
                    UniqueId =
                        existingOperationSystem?.UniqueId ?? operationSystem.UniqueId
                };
            }
        }

        public async Task<Response>  AddOperationModeAsync(OperationModeRequest request)
        {
            var dbContext = dbFactory.Create();
            using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                Machine machine;
                MachineOperationSystem previousMos = null;
                try
                {
                    machine = await unitOfWork.MachineRepository.Query(c => c.UniqueId == request.MachineUniqueId).SingleAsync();
                    previousMos = machine.MachineOperationSystem;
                }
                catch (Exception ex)
                {
                    log.LogError(0, ex, ex.Message);
                    return new Response { Code = 8, Message = ex.Message };
                }
                OperationSystem operationSystem;
                try
                {
                    operationSystem = await
                        unitOfWork.OperationSystemRepository.Query(c => c.UniqueId == request.OperationSystemUniqueId).SingleAsync();
                }
                catch (Exception ex)
                {
                    log.LogError(0, ex, ex.Message);
                    return new Response { Code = 9, Message = ex.Message };
                }
                var newMos = MachineOperationSystemMapper.ToModel(request.OperationMode, machine,
                    operationSystem);
                var existingMos =
                    unitOfWork.MachineOperationSystemRepository.GetAll().FirstOrDefault(
                        mos => mos.BootMode == newMos.BootMode &&
                               mos.EnvironmentVariables ==
                               newMos.EnvironmentVariables
                               &&
                               mos.LogicalDrives ==
                               newMos.LogicalDrives
                               &&
                               mos.Machine.UniqueId ==
                               machine.UniqueId
                               &&
                               mos.OperationSystem.UniqueId ==
                               newMos.OperationSystem.UniqueId
                               && mos.Secure == newMos.Secure
                               &&
                               mos.SerialNumber ==
                               newMos.SerialNumber
                               &&
                               mos.SystemDirectory ==
                               newMos.SystemDirectory);
                if (existingMos == null)
                {
                    if (previousMos != null)
                    {
                        unitOfWork.MachineOperationSystemRepository.Delete(previousMos);
                    }
                    unitOfWork.MachineOperationSystemRepository.Add(newMos);
                }
                try
                {
                   await unitOfWork.SaveAsync();
                }
                catch (Exception ex)
                {
                    log.LogError(0, ex, ex.Message);
                    return new Response { Code = 10, Message = ex.Message };
                }
                return new Response { Code = 0 };
            }
        }

        #endregion
    }
}