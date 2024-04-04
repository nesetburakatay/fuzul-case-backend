using AutoMapper;
using Core.DTOs.CountryDTOs;
using Core.DTOs.DistrictDTOs;
using Core.DTOs.EstateDTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping.DistrictMapping
{
    public class DistrictProfile: Profile
    {
        public DistrictProfile()
        {
            CreateMap<District, DistrictDTO>().ReverseMap();
            CreateMap<District, DistrictsOfCountries>().ReverseMap();

        }
    }
}
