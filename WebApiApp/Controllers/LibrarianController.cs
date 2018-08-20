using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Web.Http;

namespace WebApi_Library.Controllers
{
    //[Authorize(Roles = "Librarian")]                    //Contrller is only for librarian 
    //[RoutePrefix("api/Librarian")]
    public class LibrarianController : ApiController
    {
        private IBookService _bookContext;
        private ILibrarian _libContext;

        public LibrarianController(IBookService bookServ, ILibrarian libServ)
        {
            _bookContext = bookServ;
            _libContext = libServ;
        }

        [HttpGet]
        public IHttpActionResult BookList()                     //Get list of books
        {
            return Ok(_libContext.GetBookList());
        }

        [HttpGet]
        [Route("api/Librarian/Debtors")]
        public IHttpActionResult Debtors()                      //Tracing Of Debtors
        {
            return Ok(_libContext.TracingOfDebtors());
        }

        [HttpGet]
        [Route("api/Librarian/NewClientsOrders")]
        public IHttpActionResult NewClientsOrders()             //Orders For Accepting
        {
            return Ok(_libContext.OrdersForAccepting());
        }

        [HttpGet]
        [Route("api/Librarian/Book/{Id}")]
        public IHttpActionResult Book(int? id)                 //Get Book By Id
        {
            var book = _bookContext.GetItem(id.Value);

            if (book == null)
                return BadRequest();

            return Ok(book);
        }

        [HttpPost]
        public IHttpActionResult GiveTheBook(OrderDTO order)    //Librarian can give book to client, client must to make order first
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _libContext.GiveTheBook(order);

            return Created(new Uri(Request.RequestUri + "/" + order.OrderedBook), order.OrderID);
        }

        [HttpPost]
        [Route("api/Librarian/CreateBook")]
        public IHttpActionResult CreateBook(BookDTO newBook)      //Create new book
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _bookContext.CreateItem(newBook);

            return Created(new Uri(Request.RequestUri + "/" + newBook.BookId), newBook.Title);
        }

        [HttpPut]
        public void UpdateBook(int? id, BookDTO updateBook)     //Update information about book 
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);

            var book = _bookContext.GetItem(id.Value);

            if (book == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            _bookContext.EditItem(updateBook);
        }

        [HttpDelete]
        public void DeleteBook(int? id)                     //Delete book by Id
        {
            var book = _bookContext.GetItem(id.Value);
            if (book == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            _bookContext.DeleteItem(id);
        }

    }
}

