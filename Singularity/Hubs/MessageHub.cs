using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singularity.Hubs
{
    public class MessageHub : Hub
    {
        private IOnlineService _onlineService;
        private IMessageService _messageService;
        private IUserService _userService;
        public MessageHub(
            IOnlineService onlineUsers,
            IMessageService messageService,
            IUserService userService)
        {
            _onlineService = onlineUsers;
            _messageService = messageService;
            _userService = userService;
        }
        public async Task Send(MessageDTO msg)
        {
            if (_onlineService.IfUserOnline(msg.Reciever.Id))
            {
                await Clients.User(_onlineService
                    .GetConnId(msg.Reciever.Id))
                    .SendAsync("Send", msg);
            }
            await Clients.Caller
                    .SendAsync("Send", msg);
            //await _messageService.CreateMessageAsync(msg);
        }

        public override async Task OnConnectedAsync()
        {
            var context = Context.GetHttpContext();
            if (context.Request.Query.ContainsKey("access_token"))
            {
                var userId = context.Request.Query["access_token"];
                string id = userId.ToString();
                var connectionId = context.Connection.Id;
                await _onlineService
                    .ConnectUser(int
                    .Parse(id), connectionId);
                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var context = Context.GetHttpContext();
            await _onlineService.DisconnectUser(context.Connection.Id);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
