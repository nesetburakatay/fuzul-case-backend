using Core.DTOs.DistrictDTOs;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.EstateDTOs
{
    public class EstateDTO
    {
        public long Id { get; set; }
        public long FieldM2 { get; set; }
        public int BuildingAge { get; set; }
        public string EstateType { get; set; }
        public string RentType { get; set; }
        public string Dsc { get; set; }
        public decimal Price { get; set; }
        public string HeaderDsc { get; set; }

        public DistrictDTO District { get; set; }
   
    }
}
