using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Web.Http;

namespace WebAPI_Identity.Controllers
{
    //[Authorize(Roles = "Librarian")]                    //Contrller is only for librarian 
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
        public IHttpActionResult BookList()
        {
            return Ok(_libContext.GetBookList());
        }

        [HttpPost]
        public IHttpActionResult GiveTheBook(OrderDTO order)
        {
            if (!ModelState.IsValid)
                return BadRequest();
             
            _libContext.GiveTheBook(order);
             
            return Created(new Uri(Request.RequestUri + "/" + order.OrderedBook), order.OrderID);

        }

        //[HttpGet]
        //public IHttpActionResult Debtors()  
        //{
        //    return Ok(_libContext.TracingOfDebtors());      //Tracing Of Debtors
        //}       

        //[HttpGet]
        //public IHttpActionResult NewClientsOrders()
        //{
        //    return Ok(_libContext.OrdersForAccepting());     //Orders For Accepting
        //}        



        //[HttpGet] 
        //public IHttpActionResult BookList()
        //{
        //    return Ok(_bookContext.GetBooks());
        //}

        //[HttpGet]
        //public IHttpActionResult Book(int id)
        //{
        //    var book = _bookContext.GetBook(id);

        //    if (book == null)
        //        return BadRequest();

        //    return Ok(book);
        //}

        //[HttpPost]
        //public IHttpActionResult CreateBook(BookDTO newBook)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    // RECREATE
        //    //newBook.Author = new AuthorDTO() { Name = "Billy", Books = _bookContext;.GetItems() };

        //    _bookContext.CreateBook(newBook);

        //    return Created(new Uri(Request.RequestUri + "/" + newBook.BookId), newBook.Title);

        //}

        //[HttpPut]
        //public void UpdateBook(int id, BookDTO updateBook)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);

        //    var book = _bookContext.GetBook(id);
        //    if (book == null)
        //        throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

        //    _bookContext.EditBook(updateBook);

        //}

        //[HttpDelete]
        //public void DeleteBook(int id)
        //{
        //    var book = _bookContext.GetBook(id);
        //    if (book == null)
        //        throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

        //    _bookContext.DeleteBook(id);

        //}


    }
}

