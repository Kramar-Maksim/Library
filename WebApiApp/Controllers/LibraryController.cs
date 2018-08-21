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
        public IHttpActionResult BookList()         
        {
            return Ok(_context.GetItems());             //returns books from databse
        }

        [HttpGet]
        [Route("api/Library/Book{id:int}")]
        public IHttpActionResult Book(int id)     
        {
            var book = _context.GetItem(id);            //returns book(Id) from databse

            if (book == null)
                return BadRequest();

            return Ok(book);
        }
    }
}

