﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Ship.Commands.CreateShipCommand
{
    public class CreateShipCommandValidator : AbstractValidator<CreateShipCommand>
    {
        public CreateShipCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{{Name}} can not empty");

            RuleFor(x => x.Geolocation)
                .NotEmpty().WithMessage("{{Geolocation}} can not empty")
                .ChildRules(s => s.RuleFor(s => s.Latitude)
                    .GreaterThanOrEqualTo(-90).WithMessage("{{Latitude}} must greater than -90")
                    .LessThanOrEqualTo(90).WithMessage("{{Latitude}} must less than 90"))
                .ChildRules(s => s.RuleFor(s => s.Longitude)
                    .GreaterThanOrEqualTo(-90).WithMessage("{{Longitude}} must greater than -90")
                    .LessThanOrEqualTo(90).WithMessage("{{Latitude}} must less than 90"));

            RuleFor(x => x.Velocity)
                .NotEmpty().WithMessage("{{Velocity}} can not empty")
                .GreaterThanOrEqualTo(0).WithMessage("{{Velocity}} must greater than 0");
        }
    }
}
