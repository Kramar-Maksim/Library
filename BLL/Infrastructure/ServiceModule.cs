using Domain.Repository;
using Domain.UnitOfWork;
using Domain.Entities;
using Ninject.Modules;
//using Ninject.Web.Common;  

namespace BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString); 
            Bind<IRepository<Book>>().To<EFGenericRepository<Book>>();
            Bind<IRepository<Client>>().To<EFGenericRepository<Client>>();
            Bind<IRepository<Order>>().To<EFGenericRepository<Order>>();
            Bind<LibraryContext>().ToSelf();
        }
    }
}