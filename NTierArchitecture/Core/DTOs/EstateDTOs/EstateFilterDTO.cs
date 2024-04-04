using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.EstateDTOs
{
    public class EstateFilterDTO
    {
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }

        public long? MaxFieldM2 { get; set; }
        public long? MinFieldM2 { get; set; }


        public int? MaxBuildingAge { get; set; }
        public int? MimBuildingAge { get; set; }

        public long? CountryId { get; set; }
        public long? DistrictId { get; set; }


        public EstateTypeEnum? EstateType { get; set; }
        public RentTypeEnum? RentType { get; set; }

    }
}
