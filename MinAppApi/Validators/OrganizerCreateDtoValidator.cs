using FluentValidation;
using MinAppApi.Dtos.Organizer;

namespace MinAppApi.Validators
{
    public class OrganizerCreateDtoValidator : AbstractValidator<OrganizerCreateDto>
    {
        public OrganizerCreateDtoValidator()
        {
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