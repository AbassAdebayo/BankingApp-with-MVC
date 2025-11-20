using FluentValidation;

namespace BankingApp.Models.DTOs.Bank.Validation
{
    public class CreateBankRequestValidation : AbstractValidator<CreateBankRequestModel>
    {
        public CreateBankRequestValidation()
        {
            RuleFor(b => b.Name).NotEmpty().WithMessage("Bank name is required").Length(3, 50);
            RuleFor(b => b.BankBranch).NotEmpty().IsInEnum().WithMessage("Bank branch is required");

        }
    }
}
