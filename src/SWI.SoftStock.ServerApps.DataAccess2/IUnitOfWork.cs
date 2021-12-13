using System;
using System.Threading.Tasks;
using SWI.SoftStock.ServerApps.DataAccess.Common2;
using SWI.SoftStock.ServerApps.DataModel2;

namespace SWI.SoftStock.ServerApps.DataAccess2
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<SoftwareCurrentLinkedStructureUnitReadOnly> SoftwareCurrentLinkedStructureUnitReadOnlyRepository { get; }
        IRepository<MachineSoftwareLicenseReadOnly> MachineSoftwareLicenseReadOnlyRepository { get; }
        IRepository<Machine> MachineRepository { get; }
        IRepository<Software> SoftwareRepository { get; }
        IRepository<MachineSoftware> MachineSoftwareRepository { get; }
        IRepository<Publisher> PublisherRepository { get; }
        IRepository<Process> ProcessRepository { get; }
        IRepository<MachineObservedProcess> MachineObservedProcessRepository { get; }
        IRepository<OperationSystem> OperationSystemRepository { get; }
        IRepository<MachineOperationSystem> MachineOperationSystemRepository { get; }
        IRepository<MachineDomainUser> MachineDomainUserRepository { get; }
        IRepository<DomainUser> DomainUserRepository { get; }
        IRepository<StructureUnit> StructureUnitRepository { get; }
        // IRepository<User> UserRepository { get; }
        IRepository<StructureUnitUserRole> StructureUnitUserRoleRepository { get; }      
        IRepository<MachineStructureUnit> MachineStructureUnitRepository { get; }
        IRepository<MachineUser> MachineUserRepository { get; }
        IRepository<License> LicenseRepository { get; }
        IRepositoryReadOnly<LicenseType> LicenseTypeRepository { get; }
        IRepository<Document> DocumentRepository { get; }
        IRepository<LicenseSoftware> LicenseSoftwareRepository { get; }
        IRepository<LicenseAlert> LicenseAlertRepository { get; }
        IRepository<LicenseAlertUser> LicenseAlertUserRepository { get; }
        IRepository<MachineSoftwareHistory> MachineSoftwareHistoryRepository { get; }
        IRepository<LicenseMachineSoftware> LicenseMachineSoftwareRepository { get; }
        IRepository<LicenseRequest> LicenseRequestRepository { get; }
        IRepository<LicenseRequestDocument> LicenseRequestDocumentRepository { get; }
        IRepository<Observable> ObservableRepository { get; }
        IRepository<DeletedMachine> DeletedMachineRepository { get; }
        IRepository<Feedback> FeedbackRepository { get; }
        IRepository<Account> AccountRepository { get; }
        IRepository<UploadedDocument> UploadedDocumentRepository { get; }
        IRepository<RefreshToken> RefreshTokenRepository { get; }

        void Save();
        Task<int> SaveAsync();
    }
}