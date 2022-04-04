using Mc2.CrudTest.Application.Cqrs.Customers.Commands;
using Mc2.CrudTest.Application.Cqrs.Customers.Dtos;
using Mc2.CrudTest.Application.Cqrs.Customers.Queries;
using Mc2.CrudTest.WebApi.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.WebApi.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? searchText)
        {
            RequestCustomerDto requestCustomerDto = new RequestCustomerDto
            {
                SearchText = searchText
            };
            return Ok(await _mediator.Send(request: new GetAllCustomersQuery(requestCustomerDto)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(request: new GetCustomerByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            return Ok(await _mediator.Send(request: new CreateCustomerCommand(CustomerDto: customerDto)));
        }

        
    }
}
