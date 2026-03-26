using BandScope.Common.Models;
using FluentValidation;

namespace BandScope.Logic.Validators
{
    public class GenreValidator : AbstractValidator<Genre>
    {
        public GenreValidator(bool idIsRequired)
        {
            if (idIsRequired)
            {
                RuleFor(g => g.Id).GreaterThan(0);
            }

            RuleFor(g => g.Name).NotEmpty();
        }
    }
}
