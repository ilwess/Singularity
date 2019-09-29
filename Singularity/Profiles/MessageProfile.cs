using AutoMapper;
using BLL.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singularity.Profiles
{
    public class MessageProfile : Profile
    {

        public MessageProfile()
        {
            CreateMap<Message, MessageDTO>();
            CreateMap<MessageDTO, Message>();
        }
    }
}
