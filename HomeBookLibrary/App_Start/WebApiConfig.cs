using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace HomeBookLibrary
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );

            config.Routes.MapHttpRoute("BookFilter", "api/books/{authorId}/{titleId}/{genreId}/{isbn}",
                new
                {
                    controller = "Books",
                    action = "BookFilter",
                    authorId = RouteParameter.Optional,
                    titleId = RouteParameter.Optional,
                    genreId = RouteParameter.Optional,
                    isbn = RouteParameter.Optional
                });
        }
    }
}