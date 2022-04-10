using FluentValidation.TestHelper;
using IbanNet;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.WebFramework.Validators;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.XUnit
{
    public class CustomerValidationShould
    {
        private CustomerValidator Validator { get; }

        public CustomerValidationShould(IIbanValidator ibanValidator)
        {
            Validator = new CustomerValidator(ibanValidator);
        }

        [Fact]
        public async Task BankAccountNumberValidationAsync()
        {
            Customer customer = new Customer
            {
                PhoneNumber = "09124820700",
                Email = "hamidnch2007@gmail.com",
                BankAccountNumber = "12564646"
            };
            TestValidationResult<Customer>? result = await Validator.TestValidateAsync(customer);

            result.ShouldNotHaveValidationErrorFor(c => c.BankAccountNumber);
        }

        [Fact]
        public async Task EmailValidationAsync()
        {
            Customer customer = new Customer
            {
                PhoneNumber = "09124820700",
                Email = "hamidnch2007@gmail.com",
                BankAccountNumber = "12564646"
            };
            TestValidationResult<Customer>? result = await Validator.TestValidateAsync(customer);

            result.ShouldNotHaveValidationErrorFor(c => c.Email);
        }
    }
}
