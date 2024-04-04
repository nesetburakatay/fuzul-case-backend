using Core.DTOs.Auth;
using Core.Entities;
using Core.Enums;
using Core.Models;
using Core.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly StaticStringKeys _staticStringKeys;

        public AuthService(IUserService userService, IOptions<StaticStringKeys> staticStringKeys)
        {
            _userService = userService;
            _staticStringKeys = staticStringKeys.Value;
        }

        public async Task<SignInResponseDTO> SignInAsync(SignInRequestDTO signInRequestDTO)
        {
            User tempUser = await _userService.GetUserByNameAsync(signInRequestDTO.Name);

            if (tempUser is null)
                throw new NotImplementedException("User doesn't exist. Please signUp First");

            if (tempUser.Password != signInRequestDTO.Password)
                throw new NotImplementedException("PASSWORD is wrong");

            string jwt = CreateToken(tempUser.Name, tempUser.Role.ToString(), _staticStringKeys.JwtExpireTimeInMinute);
            string refreshToken = await CreateRefreshTokenAsync(tempUser.Name, _staticStringKeys.RefreshJwtExpireTimeInMinute);

            return new SignInResponseDTO() { Token = jwt, RefershToken = refreshToken };
        }

        public async Task<SignUpResponseDTO> SignUpAsync(SignUpRequestDTO signUpRequestDTO)
        {
            User tempUser = new User() { Name = signUpRequestDTO.Name, Password = signUpRequestDTO.Password, Role = Role.USER };

            if (await _userService.IsUserExistAsync(tempUser))
                throw new NotImplementedException("this user already exist");

            await _userService.AddUserAsync(tempUser);

            return new SignUpResponseDTO() { Name=signUpRequestDTO.Name,Password=signUpRequestDTO.Password};
        }

        public string CreateToken(string UserName, string Role, int ExpireTimeInMinute)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(type:"name",UserName),
                new Claim(type:"role",Role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_staticStringKeys.JwtSecretKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(ExpireTimeInMinute), signingCredentials: credential);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<string> CreateRefreshTokenAsync(string UserName, int ExpireTimeInMinute)
        {
            User tempUser = await _userService.GetUserByNameAsync(UserName);
            tempUser.RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            tempUser.RefreshTokenExpireDate = DateTime.UtcNow.AddMinutes(ExpireTimeInMinute);
            _userService.UpdateUser(tempUser);
            return tempUser.RefreshToken;
        }

        public async Task<RefreshTokenResponseDTO> RefreshTokenSignInAsync(RefreshTokenRequestDTO refreshTokenRequestDTO)
        {
            User tempUser =await _userService.GetUserByRefreshTokenAsync(refreshTokenRequestDTO.RefreshToken);

            if (tempUser != null && tempUser.RefreshTokenExpireDate > DateTime.UtcNow)
            {
                string tempToken = CreateToken(tempUser.Name, tempUser.Role.ToString(), _staticStringKeys.JwtExpireTimeInMinute);
                string tempRefreshToken = await CreateRefreshTokenAsync(tempUser.Name, _staticStringKeys.RefreshJwtExpireTimeInMinute);

                return new RefreshTokenResponseDTO() { Token = tempToken, RefreshToken = tempRefreshToken };
            }
            throw new NotImplementedException("SOMETHİNG is wrong"); ;
        }
    }
}
