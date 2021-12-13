namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Machine
    {

        public Machine()
        {
            this.NetworkAdapters = new HashSet<NetworkAdapter>();
            this.DomainUsers = new HashSet<MachineDomainUser>();
            this.MachineObservedProcesses = new HashSet<MachineObservedProcess>();
            this.MachineSoftwares = new HashSet<MachineSoftware>();
            this.LinkedUsersHistory = new HashSet<MachineUser>();
            this.LinkedStructureUnitsHistory = new HashSet<MachineStructureUnit>();
            this.MachineSoftwareHistories = new HashSet<MachineSoftwareHistory>();
            this.LicenseRequests = new HashSet<LicenseRequest>();
            this.MachineSoftwareLicenses = new HashSet<MachineSoftwareLicenseReadOnly>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MonitorCount { get; set; }
        public bool MonitorsSameDisplayFormat { get; set; }
        public int MouseButtons { get; set; }
        public string ScreenOrientation { get; set; }
        public int ProcessorCount { get; set; }
        public double MemoryTotalCapacity { get; set; }
        public System.Guid UniqueId { get; set; }
        public int CompanyId { get; set; }
        public System.Guid CompanyUniqueId { get; set; }
        public int CurrentLinkedStructureUnitId { get; set; }
        public Guid? CurrentUserId { get; set; }
        public System.DateTime LastActivityDateTime { get; set; }
        public bool IsDisabled { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int? CurrentDomainUserId { get; set; }
        public int ProcessorId { get; set; }

        public virtual ICollection<NetworkAdapter> NetworkAdapters { get; set; }
        public virtual Processor Processor { get; set; }
        public virtual ICollection<MachineDomainUser> DomainUsers { get; set; }
        public virtual ICollection<MachineObservedProcess> MachineObservedProcesses { get; set; }
        public virtual MachineOperationSystem MachineOperationSystem { get; set; }
        public virtual DomainUser CurrentDomainUser { get; set; }
        public virtual ICollection<MachineSoftware> MachineSoftwares { get; set; }
        public virtual ICollection<MachineSoftwareLicenseReadOnly> MachineSoftwareLicenses { get; set; }
        public virtual StructureUnit Company { get; set; }
        public virtual ICollection<MachineUser> LinkedUsersHistory { get; set; }
        public virtual ICollection<MachineStructureUnit> LinkedStructureUnitsHistory { get; set; }
        public virtual StructureUnit CurrentLinkedStructureUnit { get; set; }
        public virtual User CurrentUser { get; set; }
        public virtual ICollection<MachineSoftwareHistory> MachineSoftwareHistories { get; set; }
        public virtual ICollection<LicenseRequest> LicenseRequests { get; set; }
        public virtual MachineSoftwaresReadOnly MachineSoftwaresReadOnly { get; set; }
    }
}
