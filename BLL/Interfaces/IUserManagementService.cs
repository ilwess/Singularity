using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IUserManagementService
    {
        bool IsValidUser(string userName, string password);
    }
}
