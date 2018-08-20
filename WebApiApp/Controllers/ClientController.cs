using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;

namespace WebApi_Library.Controllers
{
    //[Authorize]
    public class ClientController : ApiController
    {
        private IClientService _context;

        public ClientController(IClientService service)
        {
            _context = service;
        }

        [HttpGet]
        [Authorize(Roles = "client")]
        [Route("api/Client/Profile")]
        public IHttpActionResult Profile()
        {
            // var u = User.Identity.Name;         //can use to take information from clients table
            //_context.GetItem(User.Identity.Name);

            return Ok(User.Identity.Name);
        }

    }
}
