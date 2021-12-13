using Microsoft.EntityFrameworkCore;
using SWI.SoftStock.ServerApps.DataModel2.Identity.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SWI.SoftStock.ServerApps.DataModel2
{
    public class MainDbContext : ApplicationDbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<License>()
                .HasOne(f => f.LicenseRequest);

            modelBuilder.Entity<User>()

       .Ignore(p => p.Comment)
       .Ignore(p => p.PasswordFailuresSinceLastSuccess)
       .Ignore(p => p.LastPasswordFailureDate)
       .Ignore(p => p.LastLockoutDate)
       .Ignore(p => p.ConfirmationToken)

       .Ignore(p => p.LastPasswordChangedDate)
       .Ignore(p => p.PasswordVerificationToken)
       .Ignore(p => p.PasswordVerificationTokenExpirationDate)
       .Ignore(p => p.LastLoginDate)
       .HasOne(p => p.Company).WithMany().HasForeignKey(t => t.CompanyId).IsRequired();

            modelBuilder.Entity<User>().HasMany(u => u.StructureUnitRoles).WithOne(s => s.User).HasForeignKey(s => s.UserUserId).IsRequired();
            modelBuilder.Entity<User>().HasMany(u => u.RefreshTokens).WithOne(s => s.User).HasForeignKey(s => s.UserId).IsRequired();
            modelBuilder.Entity<CustomRole>().HasMany(u => u.StructureUnitRoles).WithOne(s => s.Role).HasForeignKey(s => s.RoleRoleId).IsRequired();
            modelBuilder.Entity<StructureUnit>().HasMany(u => u.StructureUnitRoles).WithOne(s => s.StructureUnit).HasForeignKey(s => s.StructureUnitId).IsRequired();

            modelBuilder.Entity<MachineOperationSystem>().HasOne(mos => mos.Machine).WithOne(r => r.MachineOperationSystem).IsRequired();

            modelBuilder.Entity<MachineOperationSystem>().HasOne(mos => mos.OperationSystem)
              .WithMany(os => os.MachineOperationSystems).HasForeignKey(mos => mos.OperationSystemId).IsRequired();

            modelBuilder.Entity<Processor>().HasMany(p => p.Machines).WithOne(m => m.Processor).HasForeignKey(m => m.ProcessorId).IsRequired();
            modelBuilder.Entity<StructureUnit>().HasMany(p => p.CompanyMachines).WithOne(m => m.Company).HasForeignKey(m => m.CompanyId).IsRequired();
            modelBuilder.Entity<StructureUnit>().HasMany(p => p.CurrentLinkedMachines).WithOne(m => m.CurrentLinkedStructureUnit).HasForeignKey(m => m.CurrentLinkedStructureUnitId);
            modelBuilder.Entity<StructureUnit>().HasOne(p => p.ParentStructureUnit).WithMany(s => s.ChildStructureUnits).HasForeignKey(p => p.StructureUnitId);

            modelBuilder.Entity<User>().HasMany(p => p.Machines).WithOne(m => m.CurrentUser).HasForeignKey(m => m.CurrentUserId);
            modelBuilder.Entity<DomainUser>().HasMany(p => p.Machines).WithOne(m => m.CurrentDomainUser).HasForeignKey(m => m.CurrentDomainUserId);
            modelBuilder.Entity<DomainUser>().HasMany(p => p.MachineDomainUsers).WithOne(m => m.DomainUser).HasForeignKey(m => m.DomainUser_Id);

            modelBuilder.Entity<Machine>().HasMany(m => m.MachineSoftwares).WithOne(ms => ms.Machine).HasForeignKey(ms => ms.MachineId).IsRequired();
            modelBuilder.Entity<Machine>().HasMany(m => m.MachineSoftwareLicenses).WithOne(ms => ms.Machine).HasForeignKey(ms => ms.MachineId).IsRequired();


            modelBuilder.Entity<Machine>().HasOne(m => m.MachineOperationSystem).WithOne(mos => mos.Machine).IsRequired().HasForeignKey<MachineOperationSystem>(mos => mos.MachineId);//.Map(mos => mos.MapKey("MachineId"));
            modelBuilder.Entity<Machine>().HasMany(m => m.NetworkAdapters).WithOne(ms => ms.Machine).HasForeignKey(n => n.MachineId).IsRequired();
            modelBuilder.Entity<Machine>().HasMany(m => m.DomainUsers).WithOne(du => du.Machine).HasForeignKey(n => n.Machine_Id).IsRequired();
            modelBuilder.Entity<MachineSoftware>().HasMany(m => m.LicenseMachineSoftwares).WithOne(ms => ms.MachineSoftware).HasForeignKey(ms => ms.MachineSoftwareId).IsRequired();

            modelBuilder.Entity<LicenseSoftware>().HasMany(m => m.LicenseMachineSoftwares).WithOne(ms => ms.LicenseSoftware).HasForeignKey(ms => ms.LicenseSoftwareId).IsRequired();
            modelBuilder.Entity<License>().HasMany(p => p.LicenseSoftwares).WithOne(m => m.License).HasForeignKey(m => m.LicenseId).IsRequired();
            modelBuilder.Entity<License>().HasOne(p => p.LicenseRequest).WithMany(lr => lr.Licenses).HasForeignKey(l => l.LicenseRequest_Id);

            modelBuilder.Entity<User>().HasMany(p => p.LicenseRequestsAsManager).WithOne(m => m.Manager).HasForeignKey(m => m.UserUserId1).IsRequired();
            modelBuilder.Entity<User>().HasMany(p => p.LicenseRequests).WithOne(m => m.User).HasForeignKey(m => m.UserUserId).IsRequired();

            modelBuilder.Entity<User>().HasMany(p => p.LicenseAlertUsers).WithOne(m => m.User).HasForeignKey(m => m.UserUserId).IsRequired();
            modelBuilder.Entity<User>().HasMany(p => p.MachinesHistory).WithOne(m => m.User).HasForeignKey(m => m.UserUserId).IsRequired();

            modelBuilder.Entity<LicenseRequest>().HasOne(lr => lr.Machine).WithMany(l => l.LicenseRequests).HasForeignKey(lr => lr.MachineId).IsRequired();
            modelBuilder.Entity<LicenseRequest>().HasOne(lr => lr.Software).WithMany(l => l.LicenseRequests).IsRequired();
            modelBuilder.Entity<LicenseRequest>().HasOne(lr=>lr.Software).WithMany(l=>l.LicenseRequests).HasForeignKey(lr => lr.Software_Id).IsRequired();

            modelBuilder.Entity<StructureUnitUserRole>().HasOne(suur => suur.Role).WithMany(r => r.StructureUnitRoles).HasForeignKey(suur=>suur.RoleRoleId).IsRequired();
            modelBuilder.Entity<StructureUnitUserRole>().HasOne(suur => suur.StructureUnit).WithMany(r => r.StructureUnitRoles).HasForeignKey(suur => suur.StructureUnitId).IsRequired();
            modelBuilder.Entity<StructureUnitUserRole>().HasOne(suur => suur.User).WithMany(r => r.StructureUnitRoles).HasForeignKey(suur => suur.UserUserId).IsRequired();

            modelBuilder.Entity<MachineSoftwaresReadOnly>((pc =>
            {
                //pc.HasNoKey();
                pc.ToView("vMachineSoftwares");
            }));
            modelBuilder.Entity<MachineSoftwaresReadOnly>().HasOne(m => m.Machine)
                .WithOne(m => m.MachineSoftwaresReadOnly).HasForeignKey<MachineSoftwaresReadOnly>(m => m.MachineId);

            modelBuilder.Entity<MachineSoftwareLicenseReadOnly>((pc =>
            {
                pc.ToView("vMachineSoftwaresLicenses");
            }));

            modelBuilder.Entity<MachineSoftwareLicenseReadOnly>().HasOne(m => m.MachineSoftware).WithOne(m => m.MachineSoftwareLicenseReadOnly).HasForeignKey<MachineSoftwareLicenseReadOnly>(m => m.MachineSoftwareId);
            modelBuilder.Entity<MachineSoftwareLicenseReadOnly>().HasOne(m => m.Software).WithMany(m => m.MachineSoftwareLicenseReadOnlys).HasForeignKey(m => m.SoftwareId);
            modelBuilder.Entity<MachineSoftwareLicenseReadOnly>().HasOne(m => m.License).WithMany(l => l.MachineSoftwareLicenseReadOnlys).HasForeignKey(m => m.LicenseId);

            modelBuilder.Entity<SoftwareCurrentLinkedStructureUnitReadOnly>((pc =>
            {
                pc.HasKey(c => new { c.SoftwareId, c.CurrentLinkedStructureUnitId });
                pc.ToView("vSoftwareCurrentLinkedStructureUnits");
            }));
            modelBuilder.Entity<SoftwareCurrentLinkedStructureUnitReadOnly>().HasOne(m => m.Software).WithMany(m => m.SoftwareCurrentLinkedStructureUnitReadOnlys).HasForeignKey(m => m.SoftwareId);
        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<NetworkAdapter> NetworkAdapters { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<MachineObservedProcess> MachineObservedProcesses { get; set; }
        public DbSet<MachineOperationSystem> MachineOperationSystems { get; set; }
        public DbSet<MachineDomainUser> MachineDomainUsers { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<MachineSoftware> MachineSoftwares { get; set; }
        public DbSet<OperationSystem> OperationSystems { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<StructureUnit> StructureUnits { get; set; }
        public DbSet<DomainUser> DomainUsers { get; set; }
        public DbSet<StructureUnitUserRole> StructureUnitUserRoles { get; set; }
        public DbSet<MachineUser> MachineUsers { get; set; }
        public DbSet<MachineStructureUnit> MachineStructureUnits { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<LicenseType> LicenseTypes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<LicenseAlert> LicenseAlerts { get; set; }
        public DbSet<LicenseSoftware> LicenseSoftwares { get; set; }
        public DbSet<LicenseAlertUser> LicenseAlertUsers { get; set; }
        public DbSet<MachineSoftwareHistory> MachineSoftwareHistories { get; set; }
        public DbSet<LicenseMachineSoftware> LicenseMachineSoftwares { get; set; }
        public DbSet<LicenseRequest> LicenseRequests { get; set; }
        public DbSet<LicenseRequestHistory> LicenseRequestHistories { get; set; }
        public DbSet<LicenseRequestDocument> LicenseRequestDocuments { get; set; }
        public DbSet<Observable> Observables { get; set; }
        public DbSet<DeletedMachine> DeletedMachines { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UploadedDocument> UploadedDocuments { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            TrackAuditable();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            TrackAuditable();
            return base.SaveChanges();
        }

        private void TrackAuditable()
        {
            #region Option BL
            var auditableEntries = ChangeTracker.Entries<IAuditable>()
                                        .Where(e => e.State == EntityState.Added ||
                                                    e.State == EntityState.Modified);
            foreach (var entry in auditableEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity != null)
                        {
                            entry.Entity.CreatedOn = DateTime.UtcNow;
                            entry.Entity.ModifiedOn = DateTime.UtcNow;
                        }
                        break;
                    case EntityState.Modified:
                        if (entry.Entity != null)
                        {
                            entry.Entity.ModifiedOn = DateTime.UtcNow;
                        }
                        break;
                }
            }

            var uniqueEntries = ChangeTracker.Entries<IUniqueId>()
                                      .Where(e => e.State == EntityState.Added ||
                                                  e.State == EntityState.Modified);

            foreach (var entry in uniqueEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity != null)
                        {
                            entry.Entity.UniqueId = Guid.NewGuid();
                        }
                        break;
                }
            }

            #endregion
        }
    }
}