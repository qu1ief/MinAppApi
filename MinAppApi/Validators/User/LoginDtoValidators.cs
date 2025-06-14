using FluentValidation;
using MinAppApi.Dtos.User;

namespace MinAppApi.Validators.User
{
    public class LoginDtoValidators : AbstractValidator<LoginDto>
    {
        public LoginDtoValidators()
        {
            RuleFor(r => r.UserName)
                .NotEmpty()
                .WithMessage("......")
                .MaximumLength(10)
                .WithMessage("......")
                .MinimumLength(6)
                .WithMessage("........");


         


            RuleFor(r => r.Password)
           .NotEmpty()
           .WithMessage("......")
           .MinimumLength(6)
           .WithMessage("......");

         

        }


    }
}
