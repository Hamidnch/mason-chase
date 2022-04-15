using AutoMapper;
using FluentValidation;
using IbanNet;
using IbanNet.FluentValidation;
using Mc2.CrudTest.Application.Cqrs.Customers.Events;
using Mc2.CrudTest.Application.Cqrs.Customers.Notifications;
using Mc2.CrudTest.Application.Cqrs.Customers.Services;
using Mc2.CrudTest.Common.Helpers;
using Mc2.CrudTest.Common.Validators;
using Mc2.CrudTest.Domain.Dtos.Customers;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Events;
using MediatR;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Commands
{
    public record CreateCustomerCommand(CustomerDto CustomerDto) : IRequest<CustomerDto>
    {
        public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
        {
            public CreateCustomerCommandValidator(IIbanValidator ibanValidator)
            {
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
        public class CreateCustomerCommandCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
        {
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            private readonly ICustomerService _customerService;
            public CreateCustomerCommandCommandHandler(IMediator mediator, ICustomerService customerService, IMapper mapper)
            {
                _mediator = mediator;
                _customerService = customerService;
                _mapper = mapper;
            }

            public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                //Customer customer = new Customer(
                //    Guid.NewGuid(),
                //    request.CustomerDto.Firstname,
                //    request.CustomerDto.Lastname,
                //    request.CustomerDto.DateOfBirth,
                //    request.CustomerDto.PhoneNumber,
                //    request.CustomerDto.Email,
                //    request.CustomerDto.BankAccountNumber);

                Customer? customer = _mapper.Map<Customer>(request.CustomerDto);

                customer.AddDomainEvent(new CustomerRegisteredEvent(customer.Id,
                $"{customer.Firstname} {customer.Lastname}", customer.Email ?? string.Empty, customer.DateOfBirth));

                //Store customer in db
                CustomerDto? response = await _customerService.CreateAsync(customer: customer);

                // Raising Event ...
                await _mediator.Publish(
                    new CreateCustomerEvent(id: response.Id), cancellationToken);

                // Send email notification
                await _mediator.Publish(
                    new EmailNotification("Hamidnch2007@gmail.com",
                    $"New customer with email {response?.Email} created."), cancellationToken);

                return response!;
            }
        }
    }
}