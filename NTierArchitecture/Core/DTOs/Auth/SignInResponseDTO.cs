using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Auth
{
    public class SignInResponseDTO
    {
        public string Token { get; set; }
        public string RefershToken { get; set; }
    }
}
