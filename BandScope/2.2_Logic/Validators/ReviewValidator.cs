using BandScope.Common.Models;
using FluentValidation;

namespace BandScope.Logic.Validators
{
    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator(bool idIsRequired)
        {
            if (idIsRequired)
            {
                RuleFor(r => r.Id).GreaterThan(0);
            }

            RuleFor(r => r.ArtistId).NotEmpty();
            RuleFor(r => r.UserId).NotEmpty();
            RuleFor(r => r.Rating).NotEmpty();
            RuleFor(r => r.Comment).NotEmpty();
            RuleFor(r => r.CreatedAt).NotEmpty();
        }
    }
}
