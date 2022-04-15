using AutoMapper;
using FluentValidation;
using IbanNet;
using IbanNet.FluentValidation;
using Mc2.CrudTest.Application.Cqrs.Customers.Services;
using Mc2.CrudTest.Common.Helpers;
using Mc2.CrudTest.Common.Validators;
using Mc2.CrudTest.Domain.Dtos.Customers;
using Mc2.CrudTest.Domain.Entities;
using MediatR;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Commands
{
    public record UpdateCustomerCommand(CustomerDto CustomerDto) : IRequest<CustomerDto>
    {
        public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
        {
            public UpdateCustomerCommandValidator(IIbanValidator ibanValidator)
            {
                RuleFor(c => c.CustomerDto.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("The customer id must not be empty");

                RuleFor(c => c.CustomerDto.Firstname)
                    .NotEmpty().WithMessage("Please ensure you have entered the first name")
                    .Length(2, 150).WithMessage("The first name must have between 2 and 150 characters");

                RuleFor(c => c.CustomerDto.Lastname)
                    .NotEmpty().WithMessage("Please ensure you have entered the last name")
                    .Length(2, 150).WithMessage("The last name must have between 2 and 150 characters");

                RuleFor(c => c.CustomerDto.DateOfBirth)
                    .NotEmpty()
                    .Must(CommonHelper.HaveMinimumAge)
                    .WithMessage("The customer must have 18 years or more");

                RuleFor(c => c.CustomerDto.Email)
                    .NotEmpty()
                    //.EmailAddress()
                    .Must(EmailValidator.Validate)
                    .WithMessage("The customer email must be valid email address.");

                RuleFor(c => c.CustomerDto.PhoneNumber)
                    .NotEmpty()
                    .Must(MobileValidator.Validate)
                    .WithMessage("The customer mobile number is invalid.");

                RuleFor(x => x.CustomerDto.BankAccountNumber).NotNull()!.Iban(ibanValidator);
            }
        }

        public record UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
        {
            private readonly ICustomerService _customerService;
            private readonly IMapper _mapper;

            public UpdateCustomerCommandHandler(ICustomerService customerService, IMapper mapper)
            {
                _customerService = customerService;
                _mapper = mapper;
            }

            public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                Customer? customer = _mapper.Map<Customer>(request.CustomerDto);
                return await _customerService.UpdateAsync(customer);

                //CustomerDto? customerDto = await _customerService.GetByIdAsync(request.CustomerDto.Id);

                //customerDto.Firstname = request.CustomerDto.Firstname;
                //customerDto.Lastname = request.CustomerDto.Lastname;
                //customerDto.Email = request.CustomerDto.Email;
                //customerDto.DateOfBirth = request.CustomerDto.DateOfBirth;
                //customerDto.PhoneNumber = request.CustomerDto.PhoneNumber;
                //customerDto.BankAccountNumber = customerDto.BankAccountNumber;

                //return await _customerService.UpdateAsync(customerDto);
            }
        }
    }
}