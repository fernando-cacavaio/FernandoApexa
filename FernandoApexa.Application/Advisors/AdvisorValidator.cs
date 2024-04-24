using FernandoApexa.Domain;
using FluentValidation;

namespace FernandoApexa.Application.Advisors
{
    public class AdvisorValidator : AbstractValidator<Advisor>
    {
        public AdvisorValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.SIN).NotEmpty().Length(9);
            RuleFor(x => x.Address).MaximumLength(255);
            RuleFor(x => x.Phone).NotEmpty().Length(8);
        }
    }
}