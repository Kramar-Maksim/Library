using BLL.DTO;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService  
    {
        Task MakeOrder(BookDTO book, string clientName);
    }
}
