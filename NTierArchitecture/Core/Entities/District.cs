using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class District : BaseEntity
    {
        public District()
        {
            Estates =new List<Estate>();
        }
        public string DistrictName { get; set; }

        public Country Country { get; set; }
        public long CountryId { get; set; }

        public ICollection<Estate> Estates { get; set; }
    }
}
