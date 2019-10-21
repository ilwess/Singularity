using AutoMapper;
using BLL.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singularity.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<Contact, ContactDTO>()
                .ForMember(m => m.UserContactId, cfg => cfg.MapFrom(u => u.UserContactId))
                .ForMember(m => m.OwnerId, cfg => cfg.MapFrom(u => u.OwnerId));
            CreateMap<ContactDTO, Contact>();
            CreateMap<BlockedUser, BlockedUserDTO>()
                .ForMember(m => m.BlockerId, cfg => cfg.MapFrom(u => u.BlockerId))
                .ForMember(m => m.BlockedId, cfg => cfg.MapFrom(u => u.BlockedId));
            CreateMap<BlockedUserDTO, BlockedUser>();
        }
    }
}
