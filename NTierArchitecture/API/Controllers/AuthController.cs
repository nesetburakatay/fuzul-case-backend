using Core.DTOs.Auth;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<ActionResult<SignInResponseDTO>> SignIn(SignInRequestDTO signInRequestDTO)
        {
            if (!ModelState.IsValid)
                return StatusCode(409, ModelState.Where(x => x.Value.Errors.Any()).ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage)).ToArray());

            return await _authService.SignInAsync(signInRequestDTO);
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<SignUpResponseDTO>> SignUp(SignUpRequestDTO signUpRequestDTO)
        {
            if (!ModelState.IsValid)
                return StatusCode(409, ModelState.Where(x => x.Value.Errors.Any()).ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage)).ToArray());

            return await _authService.SignUpAsync(signUpRequestDTO);
        }

        [HttpPost]
        [Route("RefreshTokenSignIn")]
        public async Task<ActionResult<RefreshTokenResponseDTO>> RefreshTokenSignIn(RefreshTokenRequestDTO refreshTokenRequestDTO)
        {
            return await _authService.RefreshTokenSignInAsync(refreshTokenRequestDTO);
        }
    }
}
