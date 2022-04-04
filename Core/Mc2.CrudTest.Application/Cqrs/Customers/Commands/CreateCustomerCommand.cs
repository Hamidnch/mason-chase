using FluentValidation;
using Mc2.CrudTest.Application.Cqrs.Customers.Dtos;
using Mc2.CrudTest.Application.Cqrs.Customers.Events;
using Mc2.CrudTest.Application.Cqrs.Customers.Notifications;
using Mc2.CrudTest.Application.Cqrs.Customers.Services;
using MediatR;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Commands
{
    public record CreateCustomerCommand(CustomerDto CustomerDto) : IRequest<CustomerDto>
    {
        public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
        {
            public CreateCustomerCommandValidator()
            {
                RuleFor(x => x.CustomerDto.Firstname).NotEmpty()
                    .WithMessage("please enter the first name.");
                RuleFor(x => x.CustomerDto.Lastname).NotEmpty()
                    .WithMessage("please enter the last name.");
                RuleFor(x => x.CustomerDto.Email).NotEmpty()
                    .WithMessage("please enter the email.");
            }
        }
        public class CreateCustomerCommandCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
        {
            private readonly IMediator _mediator;
            private readonly ICustomerService _customerService;
            public CreateCustomerCommandCommandHandler(IMediator mediator, ICustomerService customerService)
            {
                _mediator = mediator;
                _customerService = customerService;
            }

            public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                CustomerDto? response = await _customerService.CreateAsync(request.CustomerDto);
                // Raising Event ...
                await _mediator.Publish(
                    new CreateCustomerEvent(id: response.Id), cancellationToken);
                await _mediator.Publish(
                    new EmailNotification("Hamidnch2007@gmail.com",
                    $"New customer with email {response?.Email} created."), cancellationToken);

                return response!;
            }
        }
    }
}