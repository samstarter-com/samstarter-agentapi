using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SWI.SoftStock.ServerApps.DataAccess2;
using SWI.SoftStock.ServerApps.DataModel2;
using System;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentServices
{
    public class CheckCompanyService 
    {
        private readonly ILogger<CheckCompanyService> log;
        private readonly MainDbContextFactory dbFactory;

        public CheckCompanyService(ILogger<CheckCompanyService> log, MainDbContextFactory dbFactory)
        {
            this.log = log;
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        public async Task<int> CheckAsync(string companyId)
        {
            if (!Guid.TryParse(companyId, out var companyUniqueId))
            {
                return 1;
            }

            var dbContext = dbFactory.Create();

            using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                var isCompanyExist = await IsCompanyExistsAsync(unitOfWork, companyUniqueId);
                if (!isCompanyExist)
                {
                    return 1;
                }

                var company = await
                    unitOfWork.StructureUnitRepository.Query(c => c.UnitType == UnitType.Company && c.UniqueId == companyUniqueId)
                        .SingleAsync();

                return (company.Account.MachineCount > company.CompanyMachines.Count) ? 0 : 22;
            }
        }

        private async Task<bool> IsCompanyExistsAsync(IUnitOfWork unitOfWork, Guid companyUniqueId)
        {
            return await
                unitOfWork.StructureUnitRepository.Query(c => c.UnitType == UnitType.Company && c.UniqueId == companyUniqueId).AnyAsync();
        }
    }
}
