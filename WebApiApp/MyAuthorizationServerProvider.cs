using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebApi_Library
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    { 
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            UserDTO userDto = new UserDTO { Email = context.UserName, Password = context.Password };

            string userRole = await UserService.Authenticate(userDto);

            if (userRole != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, userRole));
                identity.AddClaim(new Claim("username", context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
        }
        private IUserService UserService
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<IUserService>(); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

    }

}