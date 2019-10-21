using BLL.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMessageService
    {
        Task CreateMessageAsync(MessageDTO messageDTO);
        Task DeleteMessageAsync(
            MessageDTO messageToDelDTO,
            UserDTO removerDTO);
        IEnumerable<MessageDTO> GetAllMessages();
        IEnumerable<MessageDTO> GetMessages(
            Expression<Func<Message, bool>> predic);

        IEnumerable<MessageDTO> GetMessagesOfUser(int userId);
        IEnumerable<MessageDTO> GetDialog(int user1Id, int user2Id);
    }
}
