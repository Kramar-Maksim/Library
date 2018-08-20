using BLL.Interfaces;
using BLL.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(WebApi_Library.Startup))]

namespace WebApi_Library
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        { 
           
            app.CreatePerOwinContext<IUserService>(CreateUserService);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider = new MyAuthorizationServerProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = myProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService();
        }
    }
}