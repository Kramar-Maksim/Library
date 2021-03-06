﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;

namespace WebAPI_Identity.Controllers
{
    //[Authorize]
    public class ClientController : ApiController
    {
        private IClientService _context;

        public ClientController(IClientService service)
        {
            _context = service;
        }

        //[HttpGet]
        //public IHttpActionResult Profile(int id)        //returns information about user
        //{
        //    var u = User.Identity.Name;

        //    var user = _context.MyProfile(id);

        //    if (user == null)
        //        return BadRequest();

        //    return Ok(user);
        //}


        [HttpGet]
        [Authorize(Roles = "User")]
        public IHttpActionResult Profile()
        {
           // var u = User.Identity.Name;         //can use to take information from clients table
            //_context.GetItem(User.Identity.Name);

            return Ok("Values [z[z");
        }

    }
}
