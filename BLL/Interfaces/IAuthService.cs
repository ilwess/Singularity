using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        bool IsAuthenticate(TokenRequest request, out string token);
    }
}
