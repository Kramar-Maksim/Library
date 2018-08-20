using System; 
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;

namespace WebApi_Library.Controllers
{
    //[Authorize(Roles = "client")]
    public class OrderController : ApiController
    {
        private IOrderService _context;

        public OrderController(IOrderService service)
        {
            _context = service;
        }


        [HttpPost]
        public IHttpActionResult MakeOrder(BookDTO book)        //Client can Make order so librarian can give it to client
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.MakeOrder(book, User.Identity.Name);

            return Created(new Uri(Request.RequestUri + "/" + book.Title), book.Title);
        }
    }
}
