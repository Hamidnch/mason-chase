﻿using Mc2.CrudTest.Application.Cqrs.Customers.Dtos;
using Mc2.CrudTest.Application.Cqrs.Customers.Services;
using MediatR;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Commands
{
    public record DeleteCustomerCommand(int Id) : IRequest<Unit>
    {
        public record DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
        {
            private readonly ICustomerService _customerService;

            public DeleteCustomerCommandHandler(ICustomerService customerService)
            {
                _customerService = customerService;
            }

            public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                CustomerDto customerDto = await _customerService.GetByIdAsync(request.Id);

                await _customerService.DeleteAsync(customerDto);
                return Unit.Value;
            }
        }
    }
}
