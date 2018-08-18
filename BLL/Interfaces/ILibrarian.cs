using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ILibrarian
    {
        IEnumerable<BookDTO> GetBookList(); 

        void GiveTheBook(OrderDTO order);

        IEnumerable<OrderDTO> TracingOfDebtors();

        IEnumerable<OrderDTO> OrdersForAccepting();


        //void GiveTheBook(int? Bookid, ClientDTO currentClientDto);
    }
}
