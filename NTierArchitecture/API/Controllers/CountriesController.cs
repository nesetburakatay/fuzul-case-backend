using Core.DTOs.EstateDTOs;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.DTOs.CountryDTOs;
using Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.OutputCaching;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet]
        [OutputCache]
        public async Task<ActionResult<CustomResponseDTO<List<CountryDTO>>>> GetAllCountries()
        {
            List<Country> allCountries = await _countryService.GetAllCountriesAsync();
            List<CountryDTO> mappedCountries = _mapper.Map<List<CountryDTO>>(allCountries);
            return Ok(new CustomResponseDTO<List<CountryDTO>>().SetData(mappedCountries));
        }

        [OutputCache]
        [HttpGet("{id}/Districts")]
        public async Task<ActionResult<CustomResponseDTO<CountryDTO>>> GetAllCountriesWithDistrict(long id)
        {
            Country tempCountry = await _countryService.GetCountryByIdWithDistrictsAsync(id);
            CountryDTO mappedCountriy = _mapper.Map<CountryDTO>(tempCountry);
            return Ok(new CustomResponseDTO<CountryDTO>().SetData(mappedCountriy));
        }

    }
}
