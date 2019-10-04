using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public MessageService(
            IMapper mapper,
            IUnitOfWork uow)
        {
            _db = uow;
            _mapper = mapper;
        }
        public async Task CreateMessageAsync(MessageDTO messageDTO)
        {
            Message newMessage = _mapper.Map<Message>(messageDTO);
            await _db.MsgRepo.Create(newMessage);
        }

        public async Task DeleteMessageAsync(
            MessageDTO messageToDelDTO,
            UserDTO removerDTO)
        {
            Message msg = await _db.MsgRepo
                .GetById(messageToDelDTO.Id);
            User remover = await _db.UserRepo
                .GetById(removerDTO.Id);
            if(msg != null || remover != null)
            {
                if(msg.Sender == remover)
                {
                    await _db.MsgRepo.Delete(msg.Id);
                    return;
                }
                if (msg.Recievers.Contains(remover))
                {
                    msg.Recievers.Remove(remover);
                    await _db.CommitAsync();
                }
            }
        }

        public IEnumerable<MessageDTO> GetAllMessages()
        {
            IQueryable<Message> messages =
                _db.MsgRepo.GetAll();
            IEnumerable<MessageDTO> messagesDTO =
                _mapper.Map<IEnumerable<MessageDTO>>
                (messages.ToList());
            return messagesDTO;
        }

        public IEnumerable<MessageDTO> GetMessages(Expression<Func<Message, bool>> predic)
        {
            IQueryable<Message> messages =
                _db.MsgRepo.Get(predic);
            IEnumerable<MessageDTO> messagesDTO =
                _mapper.Map<IEnumerable<MessageDTO>>(
                    messages.ToList());
            return messagesDTO;
        }
    }
}
