namespace SWI.SoftStock.ServerApps.DataModel2
{
    using Identity.Models;
    using System.ComponentModel.DataAnnotations;

    public class StructureUnitUserRole : IAuditable
    {
        [Key]
        public int Id { get; set; }
        public int StructureUnitId { get; set; }
        public System.Guid RoleRoleId { get; set; }
        public System.Guid UserUserId { get; set; }    
        public virtual StructureUnit StructureUnit { get; set; }
        public virtual CustomRole Role { get; set; }
        public virtual User User { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    }
}
