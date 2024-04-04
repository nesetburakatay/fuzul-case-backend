using Core.CustomExceptions;
using Core.DTOs.CountryDTOs;
using Core.DTOs.EstateDTOs;
using Core.Entities;
using Core.Enums;
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
    public class EstateService : IEstateService
    {
        private readonly IEstateRepository _estateRepository;

        public EstateService(IEstateRepository estateRepository)
        {
            _estateRepository = estateRepository;
        }

        public Task<List<Estate>> GetAllFilteredEstatesAsync(EstateFilterDTO estateFilterDTO)
        {
            IQueryable<Estate> tempEstate = _estateRepository.xGetAll(tracking: false).Include(x => x.District).ThenInclude(x=>x.Country).AsQueryable();

            tempEstate = tempEstate.Where(x => x.isDeleted == false);

            if (estateFilterDTO.EstateType != null)
                tempEstate = tempEstate.Where(x => x.EstateType == estateFilterDTO.EstateType);

            if (estateFilterDTO.RentType != null)
                tempEstate = tempEstate.Where(x => x.RentType == estateFilterDTO.RentType);

            if (estateFilterDTO.MaxPrice != null)
                tempEstate = tempEstate.Where(x => x.Price <= estateFilterDTO.MaxPrice);

            if (estateFilterDTO.MinPrice != null)
                tempEstate = tempEstate.Where(x => x.Price >= estateFilterDTO.MinPrice);

            if (estateFilterDTO.MaxBuildingAge != null)
                tempEstate = tempEstate.Where(x => x.BuildingAge <= estateFilterDTO.MaxBuildingAge);

            if (estateFilterDTO.MimBuildingAge != null)
                tempEstate = tempEstate.Where(x => x.BuildingAge >= estateFilterDTO.MimBuildingAge);

            if (estateFilterDTO.MaxFieldM2 != null)
                tempEstate = tempEstate.Where(x => x.FieldM2 <= estateFilterDTO.MaxFieldM2);

            if (estateFilterDTO.MinFieldM2 != null)
                tempEstate = tempEstate.Where(x => x.FieldM2 >= estateFilterDTO.MinFieldM2);

            if (estateFilterDTO.DistrictId != null)
                tempEstate= tempEstate.Where(x => x.DistrictId == estateFilterDTO.DistrictId);
            else
            {
                if (estateFilterDTO.CountryId!=null)
                {
                    tempEstate = tempEstate.Where(x => x.District.CountryId == estateFilterDTO.CountryId);
                }
            }

            return tempEstate.ToListAsync();

        }

        public async Task<Estate> AddEstateAsync(Estate estate)
        {
            await _estateRepository.xAddAsync(estate);
            await _estateRepository.xSaveChangesAsync();
            return estate;
        }

        public async Task<bool> InactiveEstateAsync(long id)
        {
            Estate tempEstate = await _estateRepository.xGetByIdAsync(id);
            if (tempEstate == null)
                throw new EntityNotFoundException();
            tempEstate.isDeleted = true;
            await _estateRepository.xSaveChangesAsync();
            return true;
        }

        public async Task<FilterParamsResponseDTO> GetFilterParamsAsync()
        {
            var tempList = await _estateRepository.xGetAll().Where(x => x.isDeleted == false).ToListAsync();
            if (tempList.Count == 0)
                return new FilterParamsResponseDTO();

            var estatetypes = Enum.GetValues(typeof(EstateTypeEnum))
                                 .Cast<int>()
                                 .Select(x => new KeyValuePair<int, string>(key: x, value: Enum.GetName(typeof(EstateTypeEnum), x)))
                                 .ToList();

            var renttypes = Enum.GetValues(typeof(RentTypeEnum))
                                   .Cast<int>()
                                   .Select(x => new KeyValuePair<int, string>(key: x, value: Enum.GetName(typeof(RentTypeEnum), x)))
                                   .ToList();

            decimal maxPrice = tempList.Max(x => x.Price);
            decimal minPrice = tempList.Min(x => x.Price);

            long maxFieldM2 = tempList.Max(x => x.FieldM2);
            long minFieldM2 = tempList.Min(x => x.FieldM2);

            int maxBuildingAge = tempList.Max(x => x.BuildingAge);
            int minBuildingAge = tempList.Min(x => x.BuildingAge);

            var filterresponse = new FilterParamsResponseDTO()
            {
                MaxBuildingAge = maxBuildingAge,
                MimBuildingAge = minBuildingAge,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                MaxFieldM2 = maxFieldM2,
                MinFieldM2 = minFieldM2
            };
            filterresponse.EstateTypeList = estatetypes;
            filterresponse.RentTypeList = renttypes;

            return filterresponse;
        }





        //not implemented methodes
        public Task<List<Estate>> GetAllEstatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Estate> GetEstateByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Estate UpdateEstate(Estate estate)
        {
            throw new NotImplementedException();
        }

    }
}
