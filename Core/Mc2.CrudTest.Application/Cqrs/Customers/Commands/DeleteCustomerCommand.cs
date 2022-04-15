using FluentValidation;
using Mc2.CrudTest.Application.Cqrs.Customers.Services;
using MediatR;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Commands
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<Unit>
    {
        public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
        {
            public DeleteCustomerCommandValidator()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("The customer id must not be empty");
            }
        }
        public record DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
        {
            private readonly ICustomerService _customerService;

            public DeleteCustomerCommandHandler(ICustomerService customerService)
            {
                _customerService = customerService;
            }

            public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                //CustomerDto customerDto = await _customerService.GetByIdAsync(request.Id);
                //await _customerService.DeleteAsync(customerDto);

                await _customerService.DeleteAsync(request.Id);
                return Unit.Value;
            }
        }
    }
}
