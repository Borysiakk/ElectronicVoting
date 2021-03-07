using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ElectronicVoting.Domain.Entities
{
    public class ApplicationUser :IdentityUser
    {
        public virtual ICollection<SessionValidator> User { get; set; }
    }
}