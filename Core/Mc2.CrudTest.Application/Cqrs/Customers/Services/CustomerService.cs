using AutoMapper;
using Mc2.CrudTest.Application.Cqrs.Customers.Dtos;
using Mc2.CrudTest.Application.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IMc2Repository<Customer> _customerRepository;

        public CustomerService(IMapper mapper, IMc2Repository<Customer> customers)
        {
            _mapper = mapper;
            _customerRepository = customers;
        }

        public async Task<ResponseCustomerDto> GetAllAsync(RequestCustomerDto dto)
        {
            IQueryable<Customer> customerRepositories = _customerRepository.Table;

            if (!string.IsNullOrWhiteSpace(dto.SearchText))
            {
                customerRepositories = customerRepositories.Where(d =>
                        (d.Firstname != null && (d.Firstname.Contains(dto.SearchText)) ||
                        (d.Lastname != null && d.Lastname.Contains(dto.SearchText)) ||
                        (d.Email != null && d.Email.Contains(dto.SearchText))));
            }

            List<CustomerDto> customerList = await customerRepositories
                //.Select(customer => _mapper.Map<Customer, CustomerDto>(customer))
                .Select(p => new CustomerDto
                {
                    Id = p.Id,
                    Firstname = p.Firstname,
                    Lastname = p.Lastname,
                    Email = p.Email,
                    DateOfBirth = p.DateOfBirth,
                    PhoneNumber = p.PhoneNumber,
                    BankAccountNumber = p.BankAccountNumber
                })
                .OrderBy(p => p.Lastname)
                .ThenBy(v => v.Id)
                .ToListAsync();

            ResponseCustomerDto response = new ResponseCustomerDto
            {
                CustomerDtos = customerList,
                Rows = customerList.Count
            };
            return response;
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CustomerDto>(await _customerRepository.Table.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto customerDto)
        {
            Customer? customer = _mapper.Map<Customer>(customerDto);

            await _customerRepository.InsertAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> UpdateAsync(CustomerDto customerDto)
        {
            Customer? customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.UpdateAsync(customer);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task DeleteAsync(CustomerDto customerDto)
        {
            Customer customer = (await _customerRepository.GetByIdAsync(customerDto.Id))!;
            await _customerRepository.DeleteAsync(customer);
        }
    }
}
