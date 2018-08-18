using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Entities
{
    public class LibraryContext : DbContext// IdentityDbContext<ApplicationUser>
    {
        public LibraryContext()
            : base("DefaultConnection")
        { }

       // public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DbSet<Book> Books { get; set; } 
        public DbSet<Client> Clients { get; set; } 
        public DbSet<Order> Orders { get; set; }
    }
}
