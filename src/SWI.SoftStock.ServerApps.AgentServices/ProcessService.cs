using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.AgentServices.Mappers;
using SWI.SoftStock.ServerApps.DataAccess2;
using SWI.SoftStock.ServerApps.DataModel2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentServices
{
    public class ProcessService
    {
        private readonly ILogger<ProcessService> log;
        private readonly MainDbContextFactory dbFactory;

        public ProcessService(ILogger<ProcessService> log, MainDbContextFactory dbFactory)
        {
            this.log = log;
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        #region IProcessService Members

        public async Task<Response> AddAsync(ProcessRequest request)
        {
            var dbContext = dbFactory.Create();
            using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                MachineObservedProcess machineObservedProcess;
                try
                {
                    machineObservedProcess = await
                        unitOfWork.MachineObservedProcessRepository
                            .Query(c => c.UniqueId == request.Process.MachineObservableUniqueId)
                            .SingleAsync();
                }
                catch (Exception ex)
                {
                    log.LogError(0, ex, ex.Message);
                    return new Response {Code = 11, Message = ex.Message};
                }

                var process = new Process();
                process.Name = request.Process.Name;
                process.DateTime = request.Process.DateTime;
                process.Status = ProcessMapper.ToModelStatus(request.Process.Status);
                process.MachineObservedProcess = machineObservedProcess;

                unitOfWork.ProcessRepository.Add(process);
                try
                {
                    await unitOfWork.SaveAsync();
                }
                catch (Exception ex)
                {
                    log.LogError(0, ex, ex.Message);
                    return new Response { Code = 12, Message = ex.Message };
                }
            }
            return new Response { Code = 0 };
        }

        #endregion
    }

    internal class PublisherNameComparer : IEqualityComparer<Publisher>
    {
        public bool Equals(Publisher x, Publisher y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) ||
                Object.ReferenceEquals(y, null))
                return false;

            return x.Name.ToLower() == y.Name.ToLower();
        }

        public int GetHashCode(Publisher obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashTextual = obj.Name == null
                ? 0
                : obj.Name.GetHashCode();

            return hashTextual;
        }
    }

    internal class SoftwareComparer : IEqualityComparer<Software>
    {
        public bool Equals(Software x, Software y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) ||
                Object.ReferenceEquals(y, null))
                return false;

            return x.Name == y.Name && x.Version == y.Version && x.WindowsInstaller == y.WindowsInstaller && x.SystemComponent == y.SystemComponent && x.ReleaseType == y.ReleaseType &&
                   new PublisherNameComparer().Equals(x.Publisher, y.Publisher);
        }

        public int GetHashCode(Software obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            int hashName = obj.Name == null
                ? 0
                : obj.Name.GetHashCode();

            int hashVersion = obj.Version == null
                ? 0
                : obj.Version.GetHashCode();

            int hashWindowsInstaller = obj.WindowsInstaller == null
                ? 0
                : obj.WindowsInstaller.GetHashCode();

            int hashSystemComponent = obj.SystemComponent == null
                ? 0
                : obj.SystemComponent.GetHashCode();

            int hashReleaseType = obj.ReleaseType == null
                ? 0
                : obj.ReleaseType.GetHashCode();

            int hashPublisher = obj.Publisher == null
                ? 0
                : obj.Publisher.GetHashCode();

            return hashName ^ hashVersion ^ hashWindowsInstaller ^ hashSystemComponent ^ hashReleaseType ^ hashPublisher;
        }
    }
}