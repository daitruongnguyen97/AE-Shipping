using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Ship.Commands.UpdateShipCommand
{
    public class UpdateShipCommandValidator : AbstractValidator<UpdateShipCommand>
    {
        public UpdateShipCommandValidator()
        {
            When(s => s.Name != null, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("{{Name}} can not empty");
            });

            When(s => s.Geolocation != null, () =>
            {
                RuleFor(x => x.Geolocation)
                    .ChildRules(s => s.RuleFor(s => s.Latitude).GreaterThanOrEqualTo(0).WithMessage("{{Latitude}} must greater than 0"))
                    .ChildRules(s => s.RuleFor(s => s.Longitude).GreaterThanOrEqualTo(0).WithMessage("{{Longitude}} must greater than 0"));
            });

            When(s => s.Velocity != null, () =>
            {
                RuleFor(x => x.Velocity)
                    .GreaterThanOrEqualTo(0).WithMessage("{{Velocity}} must greater than 0");
            });

        }
    }
}
