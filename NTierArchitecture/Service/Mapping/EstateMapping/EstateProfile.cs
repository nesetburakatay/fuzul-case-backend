using AutoMapper;
using Core.DTOs.EstateDTOs;
using Core.DTOs.UserDTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping.EstateMapping
{
    public class EstateProfile:Profile
    {
        public EstateProfile()
        {
            CreateMap<Estate, EstateDTO>().ReverseMap();
            CreateMap<Estate, AddEstateRequestDTO>().ReverseMap();

        }
    }
}
