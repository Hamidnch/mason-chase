//using FluentValidation;
//using IbanNet;
//using IbanNet.FluentValidation;
//using Mc2.CrudTest.Common.Validators;
//using Mc2.CrudTest.Domain.Dtos.Customers;

//namespace Mc2.CrudTest.Domain.Validations
//{
//    public abstract class CustomerCommandValidation : AbstractValidator<CustomerDto>
//    {
//        private readonly IIbanValidator _ibanValidator;

//        protected CustomerCommandValidation(IIbanValidator ibanValidator)
//        {
//            _ibanValidator = ibanValidator;
//        }

//        protected void ValidateFirstName()
//        {
//            RuleFor(c => c.Firstname)
//                .NotEmpty().WithMessage("Please ensure you have entered the first name")
//                .Length(2, 150).WithMessage("The first name must have between 2 and 150 characters");
//        }

//        protected void ValidateLastName()
//        {
//            RuleFor(c => c.Lastname)
//                .NotEmpty().WithMessage("Please ensure you have entered the last name")
//                .Length(2, 150).WithMessage("The last name must have between 2 and 150 characters");
//        }

//        protected void ValidateDateOfBirth()
//        {
//            RuleFor(c => c.DateOfBirth)
//                .NotEmpty()
//                .Must(HaveMinimumAge)
//                .WithMessage("The customer must have 18 years or more");
//        }

//        protected void ValidateEmail()
//        {
//            RuleFor(c => c.Email)
//                .NotEmpty()
//                //.EmailAddress()
//                .Must(EmailValidator.Validate)
//                .WithMessage("The customer email must be valid email address.");
//        }

//        protected void ValidateMobile()
//        {
//            RuleFor(c => c.PhoneNumber)
//                .NotEmpty()
//                .Must(MobileValidator.Validate)
//                .WithMessage("The customer mobile number is invalid.");
//        }

//        protected void ValidateId()
//        {
//            RuleFor(c => c.Id)
//                .NotEqual(Guid.Empty)
//                .WithMessage("The customer id must not be empty");
//        }

//        protected void ValidateBankAccountNumber()
//        {
//            RuleFor(x => x.BankAccountNumber).NotNull()!.Iban(_ibanValidator);
//        }

//        private static bool HaveMinimumAge(DateTime birthDate)
//        {
//            return birthDate <= DateTime.Now.AddYears(-18);
//        }
//    }
//}
