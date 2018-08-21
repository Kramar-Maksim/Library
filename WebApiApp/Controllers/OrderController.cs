using System;
using System.Threading.Tasks;
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
        [Route("api/Order/MakeOrder")]
        public async Task<IHttpActionResult> MakeOrder(BookDTO book)        //Client can Make order so librarian can give it to client
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _context.MakeOrder(book.BookId, User.Identity.Name);

            //return Created(new Uri(Request.RequestUri + "/" + book.Title), book.Title);
            return Ok();
        }
    }
}
