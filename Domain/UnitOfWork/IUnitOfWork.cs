using Domain.Entities;
//using Domain.Identity;
//using Domain.IdentityRepository;
using Domain.Repository;
using System.Threading.Tasks;

namespace Domain.UnitOfWork
{
    public interface IUnitOfWork
    { 
        IRepository<Book> Books { get; }
        IRepository<Client> Clients { get; }
        IRepository<Order> Orders { get; }


        //ApplicationUserManager UserManager { get; }
        //IClientManager ClientManager { get; }
        //ApplicationRoleManager RoleManager { get; }

         
        Task SaveAsync();
    }
}
