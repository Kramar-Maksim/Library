using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Entities
{ 
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    { 
        public ApplicationContext()
            : base("DefaultConnection")
        { }

        public DbSet<Client> Clients { get; set; }  
        public DbSet<Book> Books { get; set; } 
        public DbSet<Order> Orders { get; set; }
    }
}
