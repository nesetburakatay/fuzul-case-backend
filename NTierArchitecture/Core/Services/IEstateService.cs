using Core.DTOs.EstateDTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IEstateService
    {
        Task<List<Estate>> GetAllEstatesAsync();
        Task<List<Estate>> GetAllFilteredEstatesAsync(EstateFilterDTO estateFilterDTO);
        Task<Estate> GetEstateByIdAsync(long id);
        Task<Estate> AddEstateAsync(Estate estate);
        Estate UpdateEstate(Estate estate);
        Task<bool> InactiveEstateAsync(long id);

        Task<FilterParamsResponseDTO> GetFilterParamsAsync();    
    }
}
