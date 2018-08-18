using BLL.DTO;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IBookService 
    {
        IEnumerable<BookDTO> GetBooks();
        void CreateBook(BookDTO bookDTO);
        void EditBook(BookDTO bookDTO);
        BookDTO GetBook(int? id);
        void DeleteBook(int? id);
    }
}
