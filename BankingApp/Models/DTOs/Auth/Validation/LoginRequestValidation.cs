using FluentValidation;

namespace BankingApp.Models.DTOs.Auth.Validation
{
    public class LoginRequestValidation : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
