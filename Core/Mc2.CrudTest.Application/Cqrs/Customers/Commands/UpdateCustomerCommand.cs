using AutoMapper;
using Mc2.CrudTest.Application.Cqrs.Customers.Dtos;
using Mc2.CrudTest.Application.Cqrs.Customers.Services;
using MediatR;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Commands
{
    public record UpdateCustomerCommand(CustomerDto CustomerDto) : IRequest<CustomerDto>
    {
        public record UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
        {
            private readonly ICustomerService _customerService;

            public UpdateCustomerCommandHandler(ICustomerService customerService)
            {
                _customerService = customerService;
            }

            public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                CustomerDto? customerDto = await _customerService.GetByIdAsync(request.CustomerDto.Id);

                customerDto.Firstname = request.CustomerDto.Firstname;
                customerDto.Lastname = request.CustomerDto.Lastname;
                customerDto.Email = request.CustomerDto.Email;
                customerDto.DateOfBirth = request.CustomerDto.DateOfBirth;
                customerDto.PhoneNumber = request.CustomerDto.PhoneNumber;
                customerDto.BankAccountNumber = customerDto.BankAccountNumber;

                return await _customerService.UpdateAsync(customerDto);
            }
        }
    }
}