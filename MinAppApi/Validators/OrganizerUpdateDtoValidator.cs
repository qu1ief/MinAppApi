using FluentValidation;
using MinAppApi.Dtos.Organizer;

namespace MinAppApi.Validators
{
    public class OrganizerUpdateDtoValidator : AbstractValidator<OrganizerUpdateDto>
    {
        public OrganizerUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Phone)
                .MaximumLength(20)
                .When(x => !string.IsNullOrEmpty(x.Phone));


        }
    }
}