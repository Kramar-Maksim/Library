using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ILibrarian
    {
        IEnumerable<BookDTO> GetBookList();

        Task GiveTheBook(OrderDTO order);

        IEnumerable<OrderDTO> TracingOfDebtors();

        IEnumerable<OrderDTO> OrdersForAccepting();
    }
}
