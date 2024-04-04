using Core.DTOs.CountryDTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.DistrictDTOs
{
    public class DistrictDTO
    {
        public long Id { get; set; }
        public string DistrictName { get; set; }
        public long CountryId { get; set; }
        public CountryDTO Country { get; set; }

    }
}
