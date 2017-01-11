using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JwtApi.Security;

namespace JwtApi.Tests
{
    [TestClass]
    public class SimpleJwtProviderTest
    {
        private IJwtProvider jwtProvider;
        public SimpleJwtProviderTest() {
            jwtProvider = new SimpleJwtProvider();
        }

        [TestMethod]
        public void CreateJwt()
        {
            string token = null;
            try
            {
                 token = jwtProvider.CreateJwt();
            }
            catch (Exception ex)
            {
                throw;
            }
            

            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void ValidateJwt()
        {
            bool valid;
            try
            {
                string token = jwtProvider.CreateJwt();
                 valid = jwtProvider.ValidateJwt(token);
            }
            catch (Exception ex)
            {
                throw;
            }


            Assert.IsTrue(valid);
        }

    }
}
