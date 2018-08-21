using Domain.Entities; 
using Domain.Repository;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }

        Task SaveAsync();
        void Save();

        IRepository<Book> Books { get; }
        IRepository<Client> Clients { get; }
        IRepository<Order> Orders { get; }
    }
}
