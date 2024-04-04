using AutoMapper;
using Core.DTOs.UserDTOs;
using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<ActionResult<AddUserResponseDTO>> GetUserById(long id)
        {
            //if (!ModelState.IsValid)
            //    return StatusCode(409, ModelState.Where(x => x.Value.Errors.Any()).ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage)).ToArray());


            User tempresponse = await _userService.GetUserByIdAsync(id);

            AddUserResponseDTO response = _mapper.Map<AddUserResponseDTO>(tempresponse);

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<AddUserResponseDTO>> AddUser(AddUserRequestDTO addUserRequestDTO)
        {
            if (!ModelState.IsValid)
                return StatusCode(409, ModelState.Where(x => x.Value.Errors.Any()).ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage)).ToArray());
           
            User tempUser=_mapper.Map<User>(addUserRequestDTO);

            User tempresponse =await _userService.AddUserAsync(tempUser);

            AddUserResponseDTO response = _mapper.Map<AddUserResponseDTO>(tempresponse);

            return response;
        }

    }
}
