using BLL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Singularity.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    class TokenAuthServiceTest
    {
        [TestMethod]
        public void IsTokenCreated()
        {
            AuthController authController = new AuthController();
            TokenRequest request = new TokenRequest();
            string token = string.Empty;
        }
    }
}
