using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    public class AccountController : ApiController
    {
        private IUserService UserService
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<IUserService>(); }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }


        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "client"
                };

                OperationDetails operationDetails = await UserService.Create(userDto);

                if (operationDetails.Succedeed)
                    return Ok("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return BadRequest("Error: try another email");  // 409 
        }

        /// <summary>
        /// Initial database
        /// </summary>
        /// <returns></returns>
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "librarian@i.ua",
                UserName = "librarian@i.ua",
                Password = "1111Aa_",
                Name = "Oksana Volodimirivna",
                Address = "Kyev",
                Role = "librarian",
            }, new List<string> { "user", "librarian" });

            await UserService.SetInitialData(new UserDTO
            {
                Email = "admin@i.ua",
                UserName = "admin@i.ua",
                Password = "1111Aa_",
                Name = "Maksim Kramar",
                Address = "Kyev",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }

    }

}