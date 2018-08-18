using System; 
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;

namespace WebApi_Library.Controllers
{
    public class OrderController : ApiController
    { 
        private IOrderService _context;

        public OrderController(IOrderService service)
        {
            _context = service;
        }


        [HttpPost]
        public IHttpActionResult MakeOrder(BookDTO book, ClientDTO client)
        {
            if (!ModelState.IsValid)
                return BadRequest();
             
            _context.MakeOrder(book, client);


            return Created(new Uri(Request.RequestUri + "/" + book.Title), book.Title);
        }
    }
}
