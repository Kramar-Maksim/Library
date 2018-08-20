using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Client ClientProfile { get; set; }
    }
}
