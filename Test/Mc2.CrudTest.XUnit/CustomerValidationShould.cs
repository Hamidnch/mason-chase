using System;
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
            Customer customer = new Customer(
                Guid.NewGuid(),
                "Hamid", "NCH",
                new DateTime(1981, 8, 10),
                "09124820700", "hamidnch2007@gmail.com", "123676446");

            TestValidationResult<Customer>? result = await Validator.TestValidateAsync(customer);

            result.ShouldNotHaveValidationErrorFor(c => c.BankAccountNumber);
        }

        [Fact]
        public async Task EmailValidationAsync()
        {
            Customer customer = new Customer(
                Guid.NewGuid(),
                "Hamid", "NCH",
                new DateTime(1981, 8, 10),
                "09124820700", "hamidnch2007@gmail.com", "123676446");

            TestValidationResult<Customer>? result = await Validator.TestValidateAsync(customer);

            result.ShouldNotHaveValidationErrorFor(c => c.Email);
        }
    }
}
