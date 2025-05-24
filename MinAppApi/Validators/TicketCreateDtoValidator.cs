using FluentValidation;
using MinAppApi.Dtos.Ticket;

namespace MinAppApi.Validators
{
    public class TicketCreateDtoValidator : AbstractValidator<TicketCreateDto>
    {
        public TicketCreateDtoValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.QuantityAvailable)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.EventId)
                .GreaterThan(0);
        }
    }
}