using AutoMapper;
using Core.DTOs.CountryDTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping.CountryMapping
{
    public class CountryProfile:Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();

        }
    }
}
