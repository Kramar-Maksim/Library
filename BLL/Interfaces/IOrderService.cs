using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IOrderService  
    {
        void MakeOrder(BookDTO book, ClientDTO client);
    }
}
