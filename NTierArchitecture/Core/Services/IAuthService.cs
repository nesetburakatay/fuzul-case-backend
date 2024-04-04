using Core.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAuthService
    {
        Task<SignInResponseDTO> SignInAsync(SignInRequestDTO signInRequestDTO);
        Task<SignUpResponseDTO> SignUpAsync(SignUpRequestDTO signUpRequestDTO);
        Task<RefreshTokenResponseDTO> RefreshTokenSignInAsync(RefreshTokenRequestDTO refreshTokenRequestDTO);
        string CreateToken(string UserName, string Role, int ExpireTimeInMinute);
        Task<string> CreateRefreshTokenAsync(string UserName, int ExpireTimeInMinute);

    }
}
