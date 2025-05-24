using FluentValidation;
using MinAppApi.Dtos.Event;

namespace MinAppApi.Validators
{
    public class EventCreateDtoValidator : AbstractValidator<EventCreateDto>
    {
        public EventCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => x.Description != null);

            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(date => date > DateTime.Now)
                .WithMessage("Event date must be in the future.");

            RuleFor(x => x.Location)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.OrganizerId)
                .GreaterThan(0);


        }
    }
}