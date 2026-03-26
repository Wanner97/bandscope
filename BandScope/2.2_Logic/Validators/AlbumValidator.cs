using BandScope.Common.Models;
using FluentValidation;

namespace BandScope.Logic.Validators
{
    public class AlbumValidator : AbstractValidator<Album>
    {
        public AlbumValidator(bool idIsRequired)
        {
            if (idIsRequired)
            {
                RuleFor(a => a.Id).GreaterThan(0);
            }

            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.ArtistId).NotEmpty();
        }
    }
}
