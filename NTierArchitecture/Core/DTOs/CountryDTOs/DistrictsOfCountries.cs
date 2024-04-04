using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.CountryDTOs
{
    public class DistrictsOfCountries
    {
        public long Id { get; set; }
        public string DistrictName { get; set; }
        public long CountryId { get; set; }
    }
}
