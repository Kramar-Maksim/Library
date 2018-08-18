using Domain.Entities;
//using Domain.Identity;
//using Domain.IdentityRepository;
using Domain.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Domain.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private LibraryContext libContext;

        public EFUnitOfWork(LibraryContext context)
        {
            libContext = context;
        }

         
        private EFGenericRepository<Book> BookRepository;
        private EFGenericRepository<Client> ClientRepository;
        private EFGenericRepository<Order> OrderRepository;
         
        public IRepository<Book> Books
        {
            get
            {
                if (BookRepository == null)
                    BookRepository = new EFGenericRepository<Book>(libContext);
                return BookRepository;
            }
        }
        public IRepository<Client> Clients
        {
            get
            {
                if (ClientRepository == null)
                    ClientRepository = new EFGenericRepository<Client>(libContext);
                return ClientRepository;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (OrderRepository == null)
                    OrderRepository = new EFGenericRepository<Order>(libContext);
                return OrderRepository;
            }
        }
        

        //public void Save()
        //{
        //    libContext.SaveChanges();
        //} 
        public async Task SaveAsync()
        {
            await libContext.SaveChangesAsync();
        }

        
    }
}
