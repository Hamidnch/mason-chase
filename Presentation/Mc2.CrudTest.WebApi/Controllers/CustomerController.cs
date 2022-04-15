using Mc2.CrudTest.Application.Cqrs.Customers.Commands;
using Mc2.CrudTest.Application.Cqrs.Customers.Events;
using Mc2.CrudTest.Application.Cqrs.Customers.Queries;
using Mc2.CrudTest.Domain.Dtos.Customers;
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
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(request: new GetCustomerByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            return Ok(await _mediator.Send(request: new CreateCustomerCommand(CustomerDto: customerDto)));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CustomerDto customerDto)
        {
            customerDto.Id = id;
            return Ok(await _mediator.Send(request: new UpdateCustomerCommand(CustomerDto: customerDto)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Publish(new CreateCustomerEvent(id: id));
            return Ok(await _mediator.Send(request: new DeleteCustomerCommand(Id: id)));
        }

    }
}
