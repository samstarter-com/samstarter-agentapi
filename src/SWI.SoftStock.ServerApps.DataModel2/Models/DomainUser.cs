namespace SWI.SoftStock.ServerApps.DataModel2
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DomainUser
    {
        public DomainUser()
        {
            this.MachineDomainUsers = new HashSet<MachineDomainUser>();
            this.Machines = new HashSet<Machine>();
        }
        [Key]
        public int Id { get; set; }
        public string DomainName { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }

        public virtual ICollection<MachineDomainUser> MachineDomainUsers { get; set; }
        public virtual ICollection<Machine> Machines { get; set; }
    }
}
