using Domain.Entities;

namespace Domain.UnitOfWork
{
    public interface IClientManager //: IDisposable
    {
        void Create(Client item);
    }
}
