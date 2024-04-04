using Core.DTOs.EstateDTOs;
using Core.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators.Estate
{
    public class EstateFilterDTOValidator: AbstractValidator<EstateFilterDTO>
    {
        public EstateFilterDTOValidator()
        {
            RuleFor(p => p.MinFieldM2).GreaterThanOrEqualTo(0).WithMessage("cant be negative");
            RuleFor(p => p.MaxFieldM2).GreaterThanOrEqualTo(0).WithMessage("cant be negative");
            RuleFor(p => p.MaxPrice).GreaterThanOrEqualTo(0).WithMessage("Price cant be negative");
            RuleFor(p => p.MinPrice).GreaterThanOrEqualTo(0).WithMessage("Price cant be negative");
            RuleFor(p => p.MaxBuildingAge).GreaterThanOrEqualTo(0).WithMessage("cant be negative");
            RuleFor(p => p.MimBuildingAge).GreaterThanOrEqualTo(0).WithMessage("cant be negative");
        }
    }
}
