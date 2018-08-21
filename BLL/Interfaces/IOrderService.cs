using BLL.DTO;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService  
    {
        Task MakeOrder(int bookId, string clientName);
    }
}
