using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Country:BaseEntity
    {
        public Country()
        {
            Districts = new List<District>();
        }
        public string CountryName { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
