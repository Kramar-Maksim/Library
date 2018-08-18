using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Domain.Entities;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        IUnitOfWork Database { get; set; } 

        public BookService(IUnitOfWork uow)
        {
            Database = uow; 
        }


        public IEnumerable<BookDTO> GetBooks()      // Get list of books
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            IEnumerable<BookDTO> testsMaped = mapper.Map<IEnumerable<Book>, List<BookDTO>>(Database.Books.GetAll());

            return testsMaped;
        }

        public void CreateBook(BookDTO bookDTO)     // Create new book
        {
            if (bookDTO == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            Book book = mapper.Map<BookDTO, Book>(bookDTO);

            Database.Books.Create(book);
            Database.SaveAsync();
        }

        public void EditBook(BookDTO bookDTO)       // Change information about the book
        {
            if (bookDTO == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            Database.Books.Update(mapper.Map<BookDTO, Book>(bookDTO));
            Database.SaveAsync();
        }

        public BookDTO GetBook(int? id)             // Get book by id
        {
            if (id == null)
                throw new ArgumentNullException();

            Book book = Database.Books.Get(id.Value);

            if (book == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper(); 
            return mapper.Map<Book, BookDTO>(Database.Books.Get(id.Value));
        }

        public void DeleteBook(int? id)             // Delete book by id
        { 
            Database.Books.Delete(id);
            Database.SaveAsync();
        }
    }
}
