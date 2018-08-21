using Domain.Entities;

namespace Domain.UnitOfWork
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }

        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }
         
        public void Create(Client item)
        {
            Database.Clients.Add(item);
            Database.SaveChanges();
        } 
    }
}
