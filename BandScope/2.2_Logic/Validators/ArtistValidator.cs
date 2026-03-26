using BandScope.Common.Models;
using FluentValidation;

namespace BandScope.Logic.Validators
{
    public class ArtistValidator : AbstractValidator<Artist>
    {
        public ArtistValidator(bool IdIsRequired)
        {
            if (IdIsRequired)
            {
                RuleFor(a => a.Id).GreaterThan(0);
            }

            RuleFor(a => a.Name).NotEmpty();
        }
    }
}
