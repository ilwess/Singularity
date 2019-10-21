using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OnlineService : IOnlineService
    {
        private IDictionary<int, string> onlineUsers;
        private IUserService _db;

        public OnlineService(IUserService service)
        {
            onlineUsers = new Dictionary<int, string>();
            _db = service;
        }
        public bool IfUserOnline(int userId)
        {
            if (onlineUsers.ContainsKey(userId))
            {
                return true;
            }
            return false;
        }

        public async Task ConnectUser(
            int userId, string connectionId)
        {
            if((await _db.GetUserByIdAsync(userId)) != null)
            {
                onlineUsers.Add(userId, connectionId);
            }
        }

        public async Task DisconnectUser(
            string connectionId)
        {
            if (onlineUsers.Values.Contains(connectionId))
            {
                foreach(var item 
                    in onlineUsers
                    .Where(i => i.Value == connectionId))
                {
                    var user =
                        await _db.GetUserByIdAsync(item.Key);
                    user.LastEnter = DateTime.Now;
                    await _db.UpdateUser(user);
                    onlineUsers.Remove(item.Key);
                }
            }
        }

        public string GetConnId(int userId)
        {
            return onlineUsers[userId];
        }

    }
}
