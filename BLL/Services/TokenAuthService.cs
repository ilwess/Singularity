using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class TokenAuthService : IAuthService
    {
        public bool IsAuthenticate(TokenRequest request, out string token)
        {
            throw new NotImplementedException();
        }
    }
}
