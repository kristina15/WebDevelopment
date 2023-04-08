using SCB.Surkova.CreditApprovalSystem.Api.Filters;
using System.Web.Http;

namespace SCB.Surkova.CreditApprovalSystem.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new MyExceptionFilter());
        }
    }
}
