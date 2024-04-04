using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICountryService 
    {
        Task<List<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryByIdWithDistrictsAsync(long id);

    }
}
