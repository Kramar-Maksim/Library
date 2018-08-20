using BLL.Interfaces;
using System.Web.Http;

namespace WebApi_Library.Controllers
{
    //[Authorize] 
    public class LibraryController : ApiController
    {
        private IBookService _context;

        public LibraryController(IBookService service)
        {
            _context = service;
        }


        [HttpGet]
        public IHttpActionResult BookList()         //returns books from databse
        {
            return Ok(_context.GetItems());
        }

        [HttpGet]
        [Route("api/Library/Book{id:int}")]
        public IHttpActionResult Book(int id)      //returns book(Id) from databse
        {
            var book = _context.GetItem(id);

            if (book == null)
                return BadRequest();

            return Ok(book);
        }
    }
}

