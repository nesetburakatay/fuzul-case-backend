using AutoMapper;
using Core.DTOs;
using Core.DTOs.EstateDTOs;
using Core.Entities;
using Core.Enums;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Service.Services;
using System.Security;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEstateService _estateService;
        public EstatesController(IMapper mapper, IEstateService estateService)
        {
            _mapper = mapper;
            _estateService = estateService;
        }

        [HttpGet]
        public async Task<ActionResult<CustomResponseDTO<List<EstateDTO>>>> GetAllFilteredEstateAsync([FromQuery] EstateFilterDTO estateFilterDTO)
        {
            List<Estate> filteredEstates = await _estateService.GetAllFilteredEstatesAsync(estateFilterDTO);
            List<EstateDTO> filteredEstatesDTO = _mapper.Map<List<EstateDTO>>(filteredEstates);
            return Ok(new CustomResponseDTO<List<EstateDTO>>().SetData(filteredEstatesDTO));
        }

        [HttpPost]
        public async Task<ActionResult<CustomResponseDTO<EstateDTO>>> AddEstateAsync(AddEstateRequestDTO addEstateRequestDTO)
        {
            Estate tempEstate = _mapper.Map<Estate>(addEstateRequestDTO);
            Estate addedEstate = await _estateService.AddEstateAsync(tempEstate);
            EstateDTO mappedEstate = _mapper.Map<EstateDTO>(addedEstate);

            return Ok(new CustomResponseDTO<EstateDTO>().SetData(mappedEstate));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomResponseDTO<NoContentResult>>> InactiveEstateAsync(long id)
        {
            await _estateService.InactiveEstateAsync(id);
            return Ok(new CustomResponseDTO<EstateDTO>());
        }

        [OutputCache]
        [HttpGet("filterparams")]
        public async Task<ActionResult<CustomResponseDTO<FilterParamsResponseDTO>>> FilterParamsAsync()
        {
            FilterParamsResponseDTO resposne = await _estateService.GetFilterParamsAsync();
            return Ok(new CustomResponseDTO<FilterParamsResponseDTO>().SetData(resposne));
        }
    }
}
