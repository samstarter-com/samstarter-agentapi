using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SWI.SoftStock.ServerApps.DataModel2
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            this.StructureUnitRoles = new HashSet<StructureUnitUserRole>();
            this.MachinesHistory = new HashSet<MachineUser>();
            this.Machines = new HashSet<Machine>();
            this.LicenseAlertUsers = new HashSet<LicenseAlertUser>();
            this.LicenseRequests = new HashSet<LicenseRequest>();
            this.LicenseRequestsAsManager = new HashSet<LicenseRequest>();
            this.Observables = new HashSet<Observable>();
            this.RefreshTokens= new HashSet<RefreshToken>();
        }
            
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comment { get; set; }
        //public bool IsApproved { get; set; }
        public int PasswordFailuresSinceLastSuccess { get; set; }
        public DateTime? LastPasswordFailureDate { get; set; }
        public DateTime? LastActivityDate { get; set; } //TODO: set this datetime on every user's activity
        public DateTime? LastLockoutDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string ConfirmationToken { get; set; }
        public System.DateTime CreateDate { get; set; }
       // public bool IsLockedOut { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }
        public string PasswordVerificationToken { get; set; }
        public DateTime? PasswordVerificationTokenExpirationDate { get; set; }       
        public SendStatus SendStatus { get; set; }
        public byte SendCount { get; set; }       
        public virtual StructureUnit Company { get; set; }
        public int CompanyId { get; set; }

        public virtual ICollection<StructureUnitUserRole> StructureUnitRoles { get; set; }
        public virtual ICollection<MachineUser> MachinesHistory { get; set; }
        public virtual ICollection<Machine> Machines { get; set; }
        public virtual ICollection<LicenseAlertUser> LicenseAlertUsers { get; set; }
        public virtual ICollection<LicenseRequest> LicenseRequests { get; set; }
        public virtual ICollection<LicenseRequest> LicenseRequestsAsManager { get; set; }
        public virtual ICollection<Observable> Observables { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(CustomUserManager manager, string authenticationType)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
        //    // Add custom user claims here
        //    return userIdentity;
        //}

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, Guid> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
    }
}
