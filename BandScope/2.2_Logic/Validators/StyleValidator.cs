using BandScope.Common.Models;
using FluentValidation;

namespace BandScope.Logic.Validators
{
    public class StyleValidator : AbstractValidator<Style>
    {
        public StyleValidator(bool idIsRequired)
        {
            if (idIsRequired)
            {
                RuleFor(s => s.Id).GreaterThan(0);
            }

            RuleFor(s => s.Name).NotEmpty();
        }
    }
}
