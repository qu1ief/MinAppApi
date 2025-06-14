using FluentValidation;
using MinAppApi.Dtos.User;

namespace MinAppApi.Validators.User
{
    public class RegisterDtoValidators  : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidators()
        {
            RuleFor(r => r.UserName)
                .NotEmpty()
                .WithMessage("......")
                .MaximumLength(10)
                .WithMessage("......")
                .MinimumLength(6)
                .WithMessage("........");


            RuleFor(r => r.FullName)
              .NotEmpty()
              .WithMessage("......")
              .MinimumLength(3)
              .WithMessage("......");


            RuleFor(r => r.Email)
           .NotEmpty()
           .WithMessage("......")
           .EmailAddress()
           .WithMessage("......");


            RuleFor(r => r.Password)
           .NotEmpty()
           .WithMessage("......")
           .MinimumLength(6)
           .WithMessage("......");

            RuleFor(r => r.ConfirmPassword)
         .NotEmpty()
         .WithMessage("......")
         .MinimumLength(6)
         .WithMessage("......");


            RuleFor(r => r.ConfirmPassword)
                .Equal(r=>r.Password);
            


        }


    }
}
