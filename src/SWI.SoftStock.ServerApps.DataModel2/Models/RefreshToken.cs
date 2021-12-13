using System;
using System.ComponentModel.DataAnnotations;

namespace SWI.SoftStock.ServerApps.DataModel2
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId{ get; set; }
        public string TokenString { get; set; }
        public DateTime ExpireAt { get; set; }
        public virtual User User { get; set; }
    }
}
