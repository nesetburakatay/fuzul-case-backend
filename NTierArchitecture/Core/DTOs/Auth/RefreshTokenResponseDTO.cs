﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Auth
{
    public class RefreshTokenResponseDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
