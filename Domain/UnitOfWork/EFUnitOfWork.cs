using Domain.Entities;
using Domain.Identity; 
using Domain.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Domain.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;

        private EFGenericRepository<Book> BookRepository;
        private EFGenericRepository<Client> ClientRepository;
        private EFGenericRepository<Order> OrderRepository;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

    
        public EFUnitOfWork()
        {
            db = new ApplicationContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
        }

        public IRepository<Book> Books
        {
            get
            {
                if (BookRepository == null)
                    BookRepository = new EFGenericRepository<Book>(db);
                return BookRepository;
            }
        }
        public IRepository<Client> Clients
        {
            get
            {
                if (ClientRepository == null)
                    ClientRepository = new EFGenericRepository<Client>(db);
                return ClientRepository;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (OrderRepository == null)
                    OrderRepository = new EFGenericRepository<Order>(db);
                return OrderRepository;
            }
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }
        public IClientManager ClientManager
        {
            get { return clientManager; }
        }
        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public  void Save ()
        {
            db.SaveChanges();
        }
    }
}
