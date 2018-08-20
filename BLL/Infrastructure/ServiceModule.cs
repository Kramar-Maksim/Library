using Domain.Entities;
using Domain.Repository;
using Domain.UnitOfWork;
using Ninject.Modules;
//using Ninject.Web.Common;  

namespace BLL.Infrastructure
{ 
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<IUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument(connectionString); 
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
            Bind<IRepository<Book>>().To<EFGenericRepository<Book>>();
            Bind<IRepository<Client>>().To<EFGenericRepository<Client>>();
            Bind<IRepository<Order>>().To<EFGenericRepository<Order>>();
            Bind<ApplicationContext>().ToSelf();
        }
    }

}