using Mc2.CrudTest.Application.Cqrs.Customers.Dtos;
using Mc2.CrudTest.Application.Cqrs.Customers.Services;
using Mc2.CrudTest.Common.Pagination;
using MediatR;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Queries
{
    public record GetAllCustomersQuery(RequestCustomerDto Dto) : IRequest<ResponseCustomerDto>
    {
        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomersQuery, ResponseCustomerDto>
        {
            private readonly ICustomerService _customerService;

            public GetAllCustomerQueryHandler(ICustomerService customerService)
            {
                _customerService = customerService;
            }

            public async Task<ResponseCustomerDto> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
            {
                ResponseCustomerDto selectedCustomers = await _customerService.GetAllAsync(request.Dto);
                IReadOnlyList<CustomerDto> customerDtos = selectedCustomers.CustomerDtos;

                IEnumerable<CustomerDto> customerDtoList = customerDtos
                //.ToPaged(request.Dto.Page, request.Dto.PageSize, out int rowCount);
                .Select(p => new CustomerDto()
                {
                    Id = p.Id,
                    Firstname = p.Firstname,
                    Lastname = p.Lastname,
                    Email = p.Email,
                    DateOfBirth = p.DateOfBirth,
                    PhoneNumber = p.PhoneNumber,
                    BankAccountNumber = p.BankAccountNumber
                }).ToList();

                ResponseCustomerDto response = new ResponseCustomerDto()
                {
                    CustomerDtos = customerDtoList.ToList(),
                    //Rows = rowCount
                };

                return response;
            }
        }
    }
}