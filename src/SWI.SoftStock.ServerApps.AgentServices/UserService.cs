using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.DataAccess2;
using SWI.SoftStock.ServerApps.DataModel2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.AgentServices
{
    public class UserService
    {
        private readonly ILogger<UserService> log;
        private readonly MainDbContextFactory dbFactory;

        public UserService(ILogger<UserService> log, MainDbContextFactory dbFactory)
        {
            this.log = log;
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        #region IUserService Members

        public async Task<Response> AddAsync(UserRequest request)
        {
            var user = new DomainUser();
            user.DomainName = request.User.UserDomainName;
            user.Name = request.User.UserName;
            var dbContext = dbFactory.Create();
            using (IUnitOfWork unitOfWork = new UnitOfWork(dbContext))
            {
                var machine =
                    unitOfWork.MachineRepository.GetAll()
                        .Single(m => m.UniqueId == request.MachineUniqueId);

                if (machine.CurrentDomainUser != null && request.User.IsEmpty)
                {
                    machine.CurrentDomainUser = null;
                }
                else
                if (machine.CurrentDomainUser != null && machine.CurrentDomainUser.Equals(user))
                {
                    var lastDateTime = machine.DomainUsers.Max(mu => mu.LastDateTime);
                    var machineuser = machine.DomainUsers.Single(mu => mu.LastDateTime == lastDateTime);
                    machineuser.LastDateTime = DateTime.UtcNow;
                }
                else
                {
                    var companyMachineUsers = unitOfWork.StructureUnitRepository.GetAll().Where(c => c.UnitType == UnitType.Company)
                        .Single(c => c.UniqueId == machine.CompanyUniqueId).CompanyMachines
                        .SelectMany(m => m.DomainUsers);

                    var existingUser = companyMachineUsers.Select(cu => cu.DomainUser)
                        .FirstOrDefault(u => u.DomainName == user.DomainName && u.Name == user.Name);
                    var machineUser = new MachineDomainUser
                    {
                        Machine = machine,
                        FirstDateTime = DateTime.UtcNow,
                        LastDateTime = DateTime.UtcNow
                    };
                    if (existingUser == null)
                    {
                        AddUser(unitOfWork, user);
                        machineUser.DomainUser = user;
                        machine.CurrentDomainUser = user;
                    }
                    else
                    {
                        machineUser.DomainUser = existingUser;
                        machine.CurrentDomainUser = existingUser;
                    }
                    unitOfWork.MachineDomainUserRepository.Add(machineUser);
                }
                try
                {
                    await unitOfWork.SaveAsync();
                }
                catch (Exception ex)
                {
                    log.LogError(0, ex, ex.Message);
                    return new Response { Code = 15, Message = ex.Message };
                }
            }
            return new Response { Code = 0 };
        }

        private void AddUser(IUnitOfWork unitOfWork, DomainUser user)
        {
            unitOfWork.DomainUserRepository.Add(user);
        }

        #endregion
    }
}