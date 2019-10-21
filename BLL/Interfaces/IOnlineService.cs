using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOnlineService
    {
        bool IfUserOnline(int userId);
        Task ConnectUser(int userId, string connectionId);
        Task DisconnectUser(string userId);
        string GetConnId(int userId);
    }
}
