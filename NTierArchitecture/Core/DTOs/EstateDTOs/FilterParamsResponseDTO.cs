using Core.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.EstateDTOs
{
    public class FilterParamsResponseDTO
    {
        public FilterParamsResponseDTO()
        {
            EstateTypeList = new List<KeyValuePair<int, string>>();
            RentTypeList = new List<KeyValuePair<int, string>>();
        }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }

        public long MaxFieldM2 { get; set; }
        public long MinFieldM2 { get; set; }


        public int MaxBuildingAge { get; set; }
        public int MimBuildingAge { get; set; }


        public List<KeyValuePair<int, string>> EstateTypeList { get; set; }
        public List<KeyValuePair<int, string>> RentTypeList { get; set; }
    }
}
