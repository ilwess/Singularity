using AutoMapper;
using BLL.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singularity.Profiles
{
    public class ContentProfile : Profile
    {
        public ContentProfile()
        {
            CreateMap<Audio, AudioDTO>();
            CreateMap<AudioDTO, AudioDTO>();

            CreateMap<Image, ImageDTO>();
            CreateMap<ImageDTO, Image>();

            CreateMap<Video, VideoDTO>();
            CreateMap<VideoDTO, Video>();
        }
    }
}
