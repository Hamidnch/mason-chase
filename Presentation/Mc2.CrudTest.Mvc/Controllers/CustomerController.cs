using AutoMapper;
using Mc2.CrudTest.Application.Cqrs.Customers.Commands;
using Mc2.CrudTest.Application.Cqrs.Customers.Events;
using Mc2.CrudTest.Application.Cqrs.Customers.Queries;
using Mc2.CrudTest.Domain.Dtos.Customers;
using Mc2.CrudTest.WebFramework.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Mvc.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CustomerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> List(string? searchText = null)
        {
            RequestCustomerDto? requestCustomerDto = new RequestCustomerDto
            {
                SearchText = searchText
            };
            ResponseCustomerDto responseCustomerDto =
                await _mediator.Send(new GetAllCustomersQuery(requestCustomerDto));

            CustomerListModel model = new CustomerListModel();
            model.AddRange(responseCustomerDto.CustomerDtos.Select(customerDto => new CustomerModel()
            {
                Id = customerDto.Id,
                Firstname = customerDto.Firstname,
                Lastname = customerDto.Lastname,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                BankAccountNumber = customerDto.BankAccountNumber,
                DateOfBirth = customerDto.DateOfBirth
            }));

            return View(model);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            CustomerDto customerDto = await _mediator.Send(new GetCustomerByIdQuery(id));
            CustomerModel model = _mapper.Map<CustomerDto, CustomerModel>(customerDto);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerModel customerModel)
        {
            CustomerDto? customerDto = _mapper.Map<CustomerModel, CustomerDto>(customerModel);
            try
            {
                if (ModelState.IsValid)
                {
                    await _mediator.Send(new CreateCustomerCommand(customerDto));
                    return RedirectToAction(nameof(List));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Problem in create new customer: " + ex.Message);
            }
            return View(customerModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            CustomerDto customerDto = await _mediator.Send(new GetCustomerByIdQuery(id));
            CustomerModel? customerModel = _mapper.Map<CustomerDto, CustomerModel>(customerDto);
            return View(customerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CustomerModel customerModel)
        {
            if (id != customerModel.Id)
            {
                return BadRequest();
            }

            CustomerDto? customerDto = _mapper.Map<CustomerModel, CustomerDto>(customerModel);
            try
            {
                if (ModelState.IsValid)
                {
                    await _mediator.Send(new UpdateCustomerCommand(customerDto));
                    return RedirectToAction(nameof(List));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Problem in editing the customer: {ex.Message}");
            }
            return View(customerModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mediator.Publish(new CreateCustomerEvent(id: id));
                await _mediator.Send(new DeleteCustomerCommand(id));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Problem in deleting the customer : {ex.Message}");
            }

            return RedirectToAction(nameof(List));
        }
    }
}
