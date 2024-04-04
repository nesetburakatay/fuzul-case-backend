using Core.CustomExceptions;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CountryService : ICountryService
    { 
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
           return await _countryRepository.xGetAll().ToListAsync();
        }
        public async Task<Country> GetCountryByIdWithDistrictsAsync(long id)
        {
            Country tempCountry = await _countryRepository.xGetAll().Where(x => x.Id == id).Include(x => x.Districts).FirstOrDefaultAsync();
            if (tempCountry==null)
                throw new EntityNotFoundException();
            return tempCountry;
        }

    }
}
