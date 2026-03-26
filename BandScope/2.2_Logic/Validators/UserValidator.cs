using BandScope.Common.Models;
using FluentValidation;

namespace BandScope.Logic.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator(bool idIsRequired)
        {
            if (idIsRequired)
            {
                RuleFor(u => u.Id).GreaterThan(0);
            }

            RuleFor(u => u.Nickname).NotEmpty();
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.PasswordHash).NotEmpty();
        }
    }
}
