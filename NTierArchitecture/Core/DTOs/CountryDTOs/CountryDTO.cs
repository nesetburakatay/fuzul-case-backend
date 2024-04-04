using Core.DTOs.DistrictDTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.CountryDTOs
{
    public class CountryDTO
    {
        public long Id { get; set; }
        public string CountryName { get; set; }
        public ICollection<DistrictsOfCountries> Districts { get; set; }
    }
}
