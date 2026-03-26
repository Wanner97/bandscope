using BandScope.Common.Models;
using FluentValidation;

namespace BandScope.Logic.Validators
{
    public class PictureValidator : AbstractValidator<Picture>
    {
        public PictureValidator(bool idIsRequired)
        {
            if (idIsRequired)
            {
                RuleFor(p => p.Id).GreaterThan(0);
            }

            RuleFor(p => p.Data).NotEmpty();
        }
    }
}
