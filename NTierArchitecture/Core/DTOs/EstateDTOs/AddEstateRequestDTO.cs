using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.EstateDTOs
{
    public class AddEstateRequestDTO
    {
        public long FieldM2 { get; set; }
        public int BuildingAge { get; set; }
        public EstateTypeEnum EstateType { get; set; }
        public RentTypeEnum RentType { get; set; }
        public string? Dsc { get; set; }
        public decimal Price { get; set; }
        public string HeaderDsc { get; set; }

        public long DistrictId { get; set; }
    }
}
