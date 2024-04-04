using AutoMapper;
using Core.DTOs.UserDTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<User, AddUserRequestDTO>().ReverseMap();
            CreateMap<User, AddUserResponseDTO>().ReverseMap();
        }
    }
}
