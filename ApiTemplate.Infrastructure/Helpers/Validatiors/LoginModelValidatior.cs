using ApiTemplate.Domain.Models.ApiModels;
using FluentValidation;


namespace ApiTemplate.Infrastructure.Helpers.Validatiors
{
    public class LoginModelValidatior : AbstractValidator<LoginModel>
    {

        public LoginModelValidatior()
        {
            RuleFor(m => m.UserName).NotNull().NotEmpty().Length(5, 25);
            RuleFor(m => m.Password).NotEmpty().Length(8, 200);
            RuleFor(x => x.Password)
            .Matches(@"[A-Z]").WithMessage("'Password' must contain at least one capital letter.");
            RuleFor(x => x.Password)
            .Matches(@"[a-z]").WithMessage("'Password' must contain at least one small letter.");
            RuleFor(x => x.Password)
           .Matches(@"\d").WithMessage("'Password' must contain at least one number.");
            RuleFor(x => x.Password)
            .Matches(@"[!@#$%^&*(),.?\\[\]]") // Corrected pattern
            .WithMessage("Password must contain at least one special character.");

            //For checking against SQL Injection
            RuleFor(x => x.UserName)
            .Matches(@"^[a-zA-Z0-9]*$").WithMessage("'UserName' contains invalid characters.")
            .Length(1, 50).WithMessage("'UserName' must be between 1 and 50 characters.");
        }
    }
}
