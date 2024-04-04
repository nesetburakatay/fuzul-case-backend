using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class StaticStringKeys
    {
        public string JwtSecretKey { get; set; }
        public string ConnectionString { get; set; }
        public int JwtExpireTimeInMinute { get; set; }
        public int RefreshJwtExpireTimeInMinute { get; set; }
    }
}
