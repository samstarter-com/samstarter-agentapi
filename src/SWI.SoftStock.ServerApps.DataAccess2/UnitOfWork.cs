using Microsoft.EntityFrameworkCore;
using SWI.SoftStock.ServerApps.DataAccess.Common2;
using SWI.SoftStock.ServerApps.DataModel2;

namespace SWI.SoftStock.ServerApps.DataAccess2
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        private IRepository<SoftwareCurrentLinkedStructureUnitReadOnly> softwareCurrentLinkedStructureUnitReadOnlyRepository;
        private IRepository<MachineSoftwareLicenseReadOnly> machineSoftwareLicenseReadOnlyRepository;
        private IRepository<MachineObservedProcess> machineObservedProcessRepository;
        private IRepository<Machine> machineRepository;
        private IRepository<MachineSoftware> machineSoftwareRepository;
        private IRepository<OperationSystem> operationSystemRepository;
        private IRepository<Process> processRepository;
        private IRepository<Processor> processorRepository;
        private IRepository<Publisher> publisherRepository;
        private IRepository<Software> softwareRepository;
        private IRepository<MachineOperationSystem> machineOperationSystemRepository;
        private IRepository<MachineDomainUser> machineDomainUserRepository;
        private IRepository<DomainUser> domainUserRepository;
        private IRepository<StructureUnit> structureUnitRepository;
        // private IRepository<User> userRepository;
        private IRepository<StructureUnitUserRole> structureUnitUserRoleRpository;
        private IRepository<MachineStructureUnit> machineStructureUnitRepository;
        private IRepository<MachineUser> machineUserRepository;
        private IRepository<License> licenseRepository;
        private IRepositoryReadOnly<LicenseType> licenseTypeRepository;
        private IRepository<Document> documentRepository;
        private IRepository<LicenseSoftware> licenseSoftwareRepository;
        private IRepository<LicenseAlert> licenseAlertRepository;
        private IRepository<LicenseAlertUser> licenseAlertUserRepository;
        private IRepository<MachineSoftwareHistory> machineSoftwareHistoryRepository;
        private IRepository<LicenseMachineSoftware> licenseMachineSoftwareRepository;
        private IRepository<LicenseRequest> licenseRequestRepository;
        private IRepository<LicenseRequestDocument> licenseRequestDocumentRepository;
        private IRepository<Observable> observableRepository;
        private IRepository<DeletedMachine> deletedMachineRepository;
        private IRepository<Feedback> feedbackRepository;
        private IRepository<Account> accountRepository;
        private IRepository<UploadedDocument> uploadedDocumentRepository;
        private IRepository<RefreshToken> refreshTokenRepository;

        public UnitOfWork(DbContext context)
            : base(context)
        {
        }

        public IRepository<Processor> ProcessorRepository => processorRepository ??= new DbContextRepository<Processor>(context);

        #region IUnitOfWork Members

        public IRepository<Process> ProcessRepository => processRepository ??= new DbContextRepository<Process>(context);

        public IRepository<MachineObservedProcess> MachineObservedProcessRepository =>
            machineObservedProcessRepository ??= new DbContextRepository<MachineObservedProcess>(context);

        public IRepository<OperationSystem> OperationSystemRepository =>
            operationSystemRepository ??= new DbContextRepository<OperationSystem>(context);

        public IRepository<MachineOperationSystem> MachineOperationSystemRepository => machineOperationSystemRepository ??= new DbContextRepository<MachineOperationSystem>(context);

        public IRepository<Machine> MachineRepository => machineRepository ??= new MachineRepository(context);

        public IRepository<Software> SoftwareRepository => softwareRepository ??= new DbContextRepository<Software>(context);

        public IRepository<MachineSoftware> MachineSoftwareRepository =>
            machineSoftwareRepository ??= new DbContextRepository<MachineSoftware>(context);

        public IRepository<Publisher> PublisherRepository => publisherRepository ??= new DbContextRepository<Publisher>(context);

        public IRepository<MachineDomainUser> MachineDomainUserRepository => machineDomainUserRepository ??= new DbContextRepository<MachineDomainUser>(context);

        public IRepository<DomainUser> DomainUserRepository => domainUserRepository ??= new DbContextRepository<DomainUser>(context);

        public IRepository<StructureUnit> StructureUnitRepository => structureUnitRepository ??= new StructureUnitRepository(context);

        public IRepository<StructureUnitUserRole> StructureUnitUserRoleRepository => structureUnitUserRoleRpository ??= new DbContextRepository<StructureUnitUserRole>(context);

        public IRepository<MachineStructureUnit> MachineStructureUnitRepository => machineStructureUnitRepository ??= new DbContextRepository<MachineStructureUnit>(context);

        public IRepository<MachineUser> MachineUserRepository => machineUserRepository ??= new DbContextRepository<MachineUser>(context);

        public IRepository<License> LicenseRepository => licenseRepository ??= new LicenseRepository(context);

        public IRepositoryReadOnly<LicenseType> LicenseTypeRepository => licenseTypeRepository ??= new DbContextRepository<LicenseType>(context);

        public IRepository<Document> DocumentRepository => documentRepository ??= new DbContextRepository<Document>(context);

        public IRepository<LicenseSoftware> LicenseSoftwareRepository => licenseSoftwareRepository ??= new DbContextRepository<LicenseSoftware>(context);

        public IRepository<LicenseAlert> LicenseAlertRepository => licenseAlertRepository ??= new LicenseAlertRepository(context);

        public IRepository<LicenseAlertUser> LicenseAlertUserRepository => licenseAlertUserRepository ??= new DbContextRepository<LicenseAlertUser>(context);

        public IRepository<MachineSoftwareHistory> MachineSoftwareHistoryRepository =>
            machineSoftwareHistoryRepository ??= new DbContextRepository<MachineSoftwareHistory>(context);

        public IRepository<LicenseMachineSoftware> LicenseMachineSoftwareRepository =>
            licenseMachineSoftwareRepository ??= new DbContextRepository<LicenseMachineSoftware>(context);

        public IRepository<LicenseRequest> LicenseRequestRepository =>
            licenseRequestRepository ??= new LicenseRequestRepository(context);

        public IRepository<LicenseRequestDocument> LicenseRequestDocumentRepository => licenseRequestDocumentRepository ??= new DbContextRepository<LicenseRequestDocument>(context);

        public IRepository<Observable> ObservableRepository => observableRepository ??= new DbContextRepository<Observable>(context);

        public IRepository<DeletedMachine> DeletedMachineRepository => deletedMachineRepository ??= new DbContextRepository<DeletedMachine>(context);

        public IRepository<Feedback> FeedbackRepository => feedbackRepository ??= new DbContextRepository<Feedback>(context);

        public IRepository<Account> AccountRepository => accountRepository ??= new DbContextRepository<Account>(context);

        public IRepository<UploadedDocument> UploadedDocumentRepository => uploadedDocumentRepository ??= new DbContextRepository<UploadedDocument>(context);

        public IRepository<RefreshToken> RefreshTokenRepository => refreshTokenRepository ??= new DbContextRepository<RefreshToken>(context);

        public IRepository<MachineSoftwareLicenseReadOnly> MachineSoftwareLicenseReadOnlyRepository => machineSoftwareLicenseReadOnlyRepository ??= new DbContextRepository<MachineSoftwareLicenseReadOnly>(context);

        public IRepository<SoftwareCurrentLinkedStructureUnitReadOnly> SoftwareCurrentLinkedStructureUnitReadOnlyRepository => softwareCurrentLinkedStructureUnitReadOnlyRepository ??= new DbContextRepository<SoftwareCurrentLinkedStructureUnitReadOnly>(context);

        #endregion
    }
}