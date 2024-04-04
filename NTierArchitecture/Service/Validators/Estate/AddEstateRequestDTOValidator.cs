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
    public class AddEstateRequestDTOValidator:AbstractValidator<AddEstateRequestDTO>
    {
        public AddEstateRequestDTOValidator()
        {
            RuleFor(p => p.FieldM2).GreaterThanOrEqualTo(0).WithMessage("Area cant be negative");
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0).WithMessage("Price cant be negative");
            RuleFor(p => p.BuildingAge).GreaterThanOrEqualTo(0).WithMessage("Age cant be negative");
            RuleFor(p => p.DistrictId).GreaterThan(0).WithMessage("DistrictId cant be negative or zero");
            RuleFor(p => p.DistrictId).LessThan(940).WithMessage("not found district ");
            RuleFor(p => p.EstateType).IsInEnum().WithMessage("not found estate type ");
            RuleFor(p => p.RentType).IsInEnum().WithMessage("not found rent type ");



        }
    }
}
