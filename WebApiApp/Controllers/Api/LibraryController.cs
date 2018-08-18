using System.Collections.Generic;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;

namespace WebAPI_Identity.Controllers
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

       
    }
}
