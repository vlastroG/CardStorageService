using CardStorageService.Models.Requests;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace CardStorageService.Models.Validators
{
    public class AuthentificationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthentificationRequestValidator()
        {
            RuleFor(c => c.Password)
                .NotNull()
                .Length(5, 50);

            RuleFor(c => c.Login)
                .NotNull()
                .Length(5, 255)
                .EmailAddress();
        }
    }
}
