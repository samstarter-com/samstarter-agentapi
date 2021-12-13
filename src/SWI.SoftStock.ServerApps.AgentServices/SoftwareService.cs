using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.AgentServices.Mappers;
using SWI.SoftStock.ServerApps.DataAccess2;
using SWI.SoftStock.ServerApps.DataModel2;
using SoftwareStatus = SWI.SoftStock.Common.Dto2.SoftwareStatus;

namespace SWI.SoftStock.ServerApps.AgentServices
{
    public class SoftwareService 
    {
        private readonly ILogger<SoftwareService> log;
        private readonly MainDbContextFactory dbFactory;

        public SoftwareService(ILogger<SoftwareService> log, MainDbContextFactory dbFactory)
        {
            this.log = log;
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        #region ISoftwareService Members

        public async Task<Response> AddAsync(SoftwareRequest request)
        {
            IEnumerable<Tuple<Software, SoftwareStatus, string>> softwareInfos =
                request.Softwares.Select(
                    sri =>
                        new Tuple<Software, SoftwareStatus, string>(SoftwareMapper.ToModel(sri.Software),
                            sri.Status,
                            sri.Software.InstallDate));
            IEnumerable<Publisher> publishers =
                softwareInfos.Select(si => si.Item1.Publisher).Where(p => p != null).GroupBy(
                    p => p.Name ?? string.Empty, StringComparer.InvariantCultureIgnoreCase).Select(
                    g => g.First());

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
                    return new Response { Code = 13, Message = ex.Message };
                }

                var existedPublishers = publishers.Join(unitOfWork.PublisherRepository.GetAll(), (existed) => existed.Name.ToLower(), (stored) => stored.Name.ToLower(), (existed, stored) => existed).Distinct().ToArray();
                var comparer = new PublisherNameComparer();
                var notExistedPublishers = publishers.Except(existedPublishers, comparer).ToArray();
                unitOfWork.PublisherRepository.AddRange(notExistedPublishers);
                try
                {
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    log.LogError(0, ex, ex.Message);
                    return new Response { Code = 16, Message = ex.Message };
                }
                var allSoftwares = softwareInfos.Select(si => si.Item1).ToArray();

                var existedSoftwares = allSoftwares
                    .Join(unitOfWork.SoftwareRepository.GetAll(),
                        (existed) =>
                            new { existed.Name, existed.Version, existed.WindowsInstaller, existed.SystemComponent, existed.ReleaseType, PublisherName = existed.Publisher != null ? existed.Publisher.Name : null },
                        (stored) => new { stored.Name, stored.Version, stored.WindowsInstaller, stored.SystemComponent, stored.ReleaseType, PublisherName = stored.Publisher != null ? stored.Publisher.Name : null },
                        (existed, stored) => new { Existed = existed, Stored = stored }).ToArray();

                var softwareComparer = new SoftwareComparer();
                var notExistedSoftwares = allSoftwares.Except(existedSoftwares.Select(exs => exs.Existed), softwareComparer);

                foreach (Software softwareInfo in notExistedSoftwares)
                {
                    softwareInfo.Publisher = softwareInfo.Publisher != null ? unitOfWork.PublisherRepository.GetAll().SingleOrDefault(p => p.Name == softwareInfo.Publisher.Name) : null;
                    unitOfWork.SoftwareRepository.Add(softwareInfo);
                }

                try
                {
                    await unitOfWork.SaveAsync();
                }
                catch (Exception ex)
                {
                    log.LogError(0, ex, ex.Message);
                    return new Response { Code = 17, Message = ex.Message };
                }

                foreach (var softwareInfoTuple in softwareInfos)
                {
                    if (softwareInfoTuple.Item2 == SoftwareStatus.Removed || softwareInfoTuple.Item2 == SoftwareStatus.Added)
                    {
                        var software = GetExistingSoftware(existedSoftwares.Select(exs => exs.Stored).Union(notExistedSoftwares).ToArray(), softwareInfoTuple.Item1);
                        var newMachineSoftwareHistory = new MachineSoftwareHistory
                        {
                            Machine = machine,
                            Software = software,
                            InstallDate = softwareInfoTuple.Item3,
                            Status = SoftwareMapper.ToModelStatus(softwareInfoTuple.Item2)
                        };

                        unitOfWork.MachineSoftwareHistoryRepository.Add(newMachineSoftwareHistory);

                        if (softwareInfoTuple.Item2 == SoftwareStatus.Removed)
                        {
                            var toRemove = unitOfWork.MachineSoftwareRepository.GetAll()
                                .SingleOrDefault(ms => ms.MachineId == machine.Id && ms.SoftwareId == software.Id && ms.InstallDate == softwareInfoTuple.Item3);
                            if (toRemove != null)
                            {
                                if (toRemove.LicenseMachineSoftwares.Any())
                                {
                                    unitOfWork.LicenseMachineSoftwareRepository.DeleteRange(
                                        toRemove.LicenseMachineSoftwares.ToArray());
                                }
                                unitOfWork.MachineSoftwareRepository.Delete(toRemove);
                            }
                        }

                        if (softwareInfoTuple.Item2 == SoftwareStatus.Added)
                        {
                            var newMachineSoftware = new MachineSoftware
                            {
                                Machine = machine,
                                Software = software,
                                InstallDate = softwareInfoTuple.Item3
                            };
                            unitOfWork.MachineSoftwareRepository.Add(newMachineSoftware);
                        }
                    }
                }

                try
                {
                   await unitOfWork.SaveAsync();
                }
                catch (Exception ex)
                {
                    log.LogError(0, ex, ex.Message);
                    return new Response { Code = 14, Message = ex.Message };
                }
            }
            return new Response { Code = 0 };
        }

        #endregion

        private Software GetExistingSoftware(IEnumerable<Software> softwares, Software software)
        {
            string publisherName = software.Publisher != null ? software.Publisher.Name.ToLower() : string.Empty;
            bool hasPublisher = software.Publisher != null;

            Func<Software, bool> wherePredicate = item => item.Name == software.Name
                                                          && item.Version == software.Version
                                                          && item.WindowsInstaller == software.WindowsInstaller
                                                          && item.SystemComponent == software.SystemComponent
                                                          && item.ReleaseType == software.ReleaseType
                                                          &&
                                                          ((item.Publisher != null &&
                                                            (item.Publisher.Name.ToLower() == publisherName)) ||
                                                           (item.Publisher == null && !hasPublisher));


            Software existingItem = softwares.FirstOrDefault(wherePredicate);
            return existingItem;
        }
    }
}