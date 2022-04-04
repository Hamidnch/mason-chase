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
        public async Task<IActionResult> GetAll(string searchTest = "")
        {
            RequestCustomerDto customerDto = new RequestCustomerDto
            {
                SearchText = searchTest
            };
            return Ok(await _mediator.Send(request: new GetAllCustomersQuery(customerDto)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            return Ok(await _mediator.Send(request: new CreateCustomerCommand(customerDto)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(request: new GetCustomerByIdQuery(id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerDto customerDto)
        {
            customerDto.Id = id;
            return Ok(await _mediator.Send(new UpdateCustomerCommand(CustomerDto: customerDto)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(request: new DeleteCustomerCommand(id)));
        }
    }
}
