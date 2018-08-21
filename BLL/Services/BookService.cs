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

        /// <summary>
        /// Get list of books
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookDTO> GetItems()      
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            IEnumerable<BookDTO> testsMaped = mapper.Map<IEnumerable<Book>, List<BookDTO>>(Database.Books.GetAll());

            return testsMaped;
        }

        /// <summary>
        /// Create new book
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <returns></returns>
        public async Task CreateItem(BookDTO bookDTO)
        {
            if (bookDTO == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
           
            Database.Books.Create(mapper.Map<BookDTO, Book>(bookDTO));
            await Database.SaveAsync();
        }

        /// <summary>
        /// Change information about the book
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <returns></returns>
        public async Task EditItem(BookDTO bookDTO)       
        {
            if (bookDTO == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            Database.Books.Update(mapper.Map<BookDTO, Book>(bookDTO));
            await Database.SaveAsync();
        }

        /// <summary>
        /// Get book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookDTO GetItem(int? id)             
        {
            if (id == null)
                throw new ArgumentNullException();

            Book book = Database.Books.Get(id.Value);

            if (book == null)
                throw new ArgumentNullException();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            return mapper.Map<Book, BookDTO>(Database.Books.Get(id.Value));
        }

        /// <summary>
        /// Delete book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteItem(int? id)            
        {
            Database.Books.Delete(id.Value);
            await Database.SaveAsync();
        }
    }
}
