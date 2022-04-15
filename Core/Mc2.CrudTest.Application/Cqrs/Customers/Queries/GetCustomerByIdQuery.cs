using AutoMapper;
using Mc2.CrudTest.Application.Cqrs.Customers.Services;
using Mc2.CrudTest.Domain.Dtos.Customers;
using MediatR;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Queries
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto>
    {
        public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
        {
            private readonly ICustomerService _playerService;
            private readonly IMapper _mapper;

            public GetCustomerByIdQueryHandler(ICustomerService playerService, IMapper mapper)
            {
                _playerService = playerService;
                _mapper = mapper;
            }

            public async Task<CustomerDto> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
            {
                return await _playerService.GetByIdAsync(query.Id);
            }
        }
    }
}
