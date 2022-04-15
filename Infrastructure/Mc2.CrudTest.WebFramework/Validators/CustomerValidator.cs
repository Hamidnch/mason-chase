using FluentValidation;
using IbanNet;
using IbanNet.FluentValidation;
using Mc2.CrudTest.Common.Validators;
using Mc2.CrudTest.Domain.Entities;

namespace Mc2.CrudTest.WebFramework.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator(IIbanValidator ibanValidator)
        {
            RuleFor(x => x.BankAccountNumber).NotNull()!.Iban(ibanValidator);
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email must not be empty");
            RuleFor(x => x.Email)
                .Must(EmailValidator.Validate!)
                .WithMessage("Email is invalid.");
        }
    }
}
