using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.AgentServices.Helpers;
using SWI.SoftStock.ServerApps.AgentServices.Mappers;
using SWI.SoftStock.ServerApps.DataAccess2;
using SWI.SoftStock.ServerApps.DataModel2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentServices
{
    public class MachineService
    {
        private readonly ILogger<MachineService> log;
        private readonly MainDbContextFactory dbFactory;

        public MachineService(ILogger<MachineService> log, MainDbContextFactory dbFactory)
        {
            this.log = log;
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        private async Task<Tuple<bool, int>> TryGetCompanyIdAsync(Guid uniqueId)
        {
            var dbContext = dbFactory.Create();
            using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                var company = await
                    unitOfWork.StructureUnitRepository.Query(c => c.UnitType == UnitType.Company && c.UniqueId == uniqueId)
                        .SingleOrDefaultAsync();
                return company == null ? new Tuple<bool, int>(false, 0) : new Tuple<bool, int>(true, company.Id);
            }
        }

        private async Task<Response> GetMachineStatusAsync(Guid uniqueId, Machine existed, IUnitOfWork unitOfWork)
        {
            if (existed == null)
            {
                var deletedMachine = await
                    unitOfWork.DeletedMachineRepository.Query(m => m.UniqueId == uniqueId).
                        SingleOrDefaultAsync();
                if (deletedMachine != null)
                {
                    return new Response { Code = 20, Message = "Machine is deleted", UniqueId = uniqueId };
                }
                log.LogError("Machine not exist. uniqueId:{0}", uniqueId);
                return new Response { Code = 18, Message = "Machine not exist", UniqueId = uniqueId };
            }
            return existed.IsDisabled ? new Response { Code = 21, Message = "Machine is disabled", UniqueId = uniqueId } : new Response();
        }

        #region IMachineService Members

        public async Task<Response> AddAsync(MachineRequest value)
        {
            var serviceRequest = Guid.NewGuid();
            Machine machine;
            try
            {
                machine = MachineMapper.ToModel(value);
            }
            catch (NullReferenceException e)
            {
                var ms = new MemorySerializer<MachineRequest>();
                var xml = ms.SerializeObject(value);
                log.LogError("Service request id:{0} MachineRequest:{1}", serviceRequest, xml);
                log.LogError($"Service request id:{serviceRequest}", e);
                return new Response { Code = 19, Message = e.Message };
            }
            try
            {
                var (exist, companyId) = await TryGetCompanyIdAsync(value.Machine.CompanyUniqueId);
                if (exist)
                {
                    machine.CompanyId = companyId;
                }
                else
                {
                    var message = $"Company not exists. CompanyUniqueId:{value.Machine.CompanyUniqueId}";
                    log.LogWarning(message);
                    return new Response
                    {
                        Code = 2,
                        Message = message
                    };
                }
            }
            catch (Exception ex)
            {
                log.LogError(0, ex, ex.Message);
                return new Response { Code = 2, Message = ex.ToString() };
            }

            var dbContext = dbFactory.Create();

            try
            {
                using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
                {
                    if (machine.UniqueId == Guid.Empty)
                    {
                        var canAddMachine = CanAddMachine(machine.CompanyId);
                        if (!canAddMachine)
                        {
                            return new Response { Code = 22 };
                        }
                        machine.CurrentLinkedStructureUnitId = machine.CompanyId;
                        machine.LastActivityDateTime = DateTime.UtcNow;
                        unitOfWork.MachineRepository.Add(machine);
                    }
                    else
                    {
                        var existed =
                            unitOfWork.MachineRepository.Query(m => m.UniqueId == machine.UniqueId).SingleOrDefault();
                        var response = await GetMachineStatusAsync(machine.UniqueId, existed, unitOfWork);
                        if (response.Code != 0)
                        {
                            return response;
                        }
                        machine.Id = existed.Id;
                        machine.CurrentLinkedStructureUnitId = existed.CurrentLinkedStructureUnitId;
                        machine.CurrentUserId = existed.CurrentUserId;
                        machine.CreatedOn = existed.CreatedOn;
                        machine.ModifiedOn = existed.ModifiedOn;
                        machine.IsDisabled = existed.IsDisabled;
                        machine.LastActivityDateTime = DateTime.UtcNow;
                        if (machine.Processor != null && machine.Processor.Id != 0)
                        {
                            machine.ProcessorId = machine.Processor.Id;
                        }
                        unitOfWork.MachineRepository.Update(machine, machine.Id);
                    }
                    await unitOfWork.SaveAsync();
                    return new Response { Code = 0, UniqueId = machine.UniqueId };
                }
            }
            catch (Exception ex)
            {
                log.LogError(0, ex, ex.Message);
                if (ex.InnerException != null)
                {
                    log.LogError(0, ex.InnerException, ex.InnerException.Message);
                }
                return new Response { Code = 1, Message = ex.Message };
            }
        }

        private bool CanAddMachine(int companyId)
        {
            var dbContext = dbFactory.Create();
            using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                var company = unitOfWork.StructureUnitRepository.GetById(companyId);
                if (company.UnitType != UnitType.Company)
                {
                    throw new Exception("not company");
                }
                return company.Account.MachineCount > company.CompanyMachines.Count;
            }
        }

        public async Task<DataResponse> GetDataAsync(Guid value)
        {
            var dbContext = dbFactory.Create();
            using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                var machine = await unitOfWork.MachineRepository.Query(m => m.UniqueId == value).SingleOrDefaultAsync();
                if (machine == null)
                {
                    return new DataResponse() { Code = 1 };
                }

                var machineObservedProcesses =
                    unitOfWork.MachineObservedProcessRepository.Query(c => c.Machine.UniqueId == value);
                var processes = await
                    machineObservedProcesses.Select(
                        mop => new ObservedProcessDto { Id = mop.UniqueId, ProcessName = mop.Observable.ProcessName }).ToArrayAsync();

                var result = new DataResponse
                {
                    ObservedProcesses = processes,
                    AgentData = new AgentDataDto() { Interval = machine.Company.Account.DefaultAgentInterval }
                };
                return result;
            }
        }

        public async Task<Response> SetActivityAsync(Guid value)
        {
            var dbContext = dbFactory.Create();
            using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                var existed = await unitOfWork.MachineRepository.Query(m => m.UniqueId == value).SingleOrDefaultAsync();
                var response = await GetMachineStatusAsync(value, existed, unitOfWork);
                if (response.Code != 0)
                {
                    return response;
                }
                existed.LastActivityDateTime = DateTime.UtcNow;
                unitOfWork.MachineRepository.Update(existed, existed.Id);
                await unitOfWork.SaveAsync();
            }
            return new Response { Code = 0, UniqueId = value };
        }

        #endregion
    }
}