using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JwtApi.Controllers;

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
    }
}
