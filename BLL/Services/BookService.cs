using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Domain.Entities;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        EFUnitOfWork Database { get; set; }

        public BookService(EFUnitOfWork uow)
        {
            Database = uow;
        }


        public IEnumerable<BookDTO> GetItems()      // Get list of books
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            IEnumerable<BookDTO> testsMaped = mapper.Map<IEnumerable<Book>, List<BookDTO>>(Database.Books.GetAll());

            return testsMaped;
        }

        public async Task CreateItem(BookDTO bookDTO)     // Create new book
        {
            if (bookDTO == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            Book book = mapper.Map<BookDTO, Book>(bookDTO);

            Database.Books.Create(book);
            await Database.SaveAsync();
        }

        public async Task EditItem(BookDTO bookDTO)       // Change information about the book
        {
            if (bookDTO == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            Database.Books.Update(mapper.Map<BookDTO, Book>(bookDTO));
            await Database.SaveAsync();
        }

        public BookDTO GetItem(int? id)             // Get book by id
        {
            if (id == null)
                throw new ArgumentNullException();

            Book book = Database.Books.Get(id.Value);

            if (book == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            return mapper.Map<Book, BookDTO>(Database.Books.Get(id.Value));
        }

        public async Task DeleteItem(int? id)             // Delete book by id
        {
            Database.Books.Delete(id.Value);
            await Database.SaveAsync();
        }
    }
}
