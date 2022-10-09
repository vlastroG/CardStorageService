using CardStorageService.Models.Requests;
using FluentValidation;

namespace CardStorageService.Models.Validators
{
    public class CreateCardRequestValidator : AbstractValidator<CreateCardRequest>
    {
        public CreateCardRequestValidator()
        {
            RuleFor(x => x.ClientId).NotNull().GreaterThan(0);

            RuleFor(x => x.CardN).NotNull().Length(16, 20).Matches("^[0-9]+$");

            RuleFor(x => x.Name).NotNull().Length(5, 50);

            RuleFor(x => x.CVV2).NotNull().Length(5, 50);

            RuleFor(x => x.ExpDate).NotNull().GreaterThan(DateTime.Now + new TimeSpan(365, 0, 0, 0));
        }
    }
}
