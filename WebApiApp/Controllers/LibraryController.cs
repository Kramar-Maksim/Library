using System;
using System.Collections.Generic;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;

namespace WebApi_Library.Controllers
{
    public class LibraryController : ApiController
    {
        private IBookService _context;

        public LibraryController(IBookService service)
        {
            _context = service; 
        }


        [HttpGet]
        public IHttpActionResult BookList()
        {
            return Ok(_context.GetBooks());     //returns books from databse
        }

        [HttpGet]
        public IHttpActionResult Book(int id)
        {
            var book = _context.GetBook(id);

            if (book == null)
                return BadRequest();

            return Ok(book);
        }
        [HttpPost]
        public IHttpActionResult CreateBook(BookDTO newBook)
        {
            if (!ModelState.IsValid)
                return BadRequest();
              
            _context.CreateBook(newBook);

            return Created(new Uri(Request.RequestUri + "/" + newBook.BookId), newBook.Title);

        }

        [HttpPut]
        public void UpdateBook(int id, BookDTO updateBook)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);

            var book = _context.GetBook(id);
            if (book == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            _context.EditBook(updateBook);

        }

        [HttpDelete]
        public void DeleteBook(int id)
        {
            var book = _context.GetBook(id);
            if (book == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            _context.DeleteBook(id);

        }

    }
}
