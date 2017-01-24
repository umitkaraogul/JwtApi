using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JwtApi.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace JwtApi.Tests
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void Get()
        {
            LoginController controller = new LoginController();

            string token = controller.Get("admin", "admin");

            Assert.IsNotNull(token);
        }
        [TestMethod]
        public void GetUsingRequest()
        {
            LoginController controller = new LoginController();
            controller.Request = new HttpRequestMessage {
                RequestUri =new Uri("http://localhost/api/products")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });


            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "login" } });


            string token = controller.Get("admin", "admin");

            Assert.IsNotNull(token);
        }
    }
}
