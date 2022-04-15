using Mc2.CrudTest.Domain.Dtos.Customers;
using Mc2.CrudTest.Domain.Entities;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Services;

public interface ICustomerService
{
    Task<ResponseCustomerDto> GetAllAsync(RequestCustomerDto dto);
    Task<CustomerDto> GetByIdAsync(Guid id);
    Task<CustomerDto> CreateAsync(Customer customer);
    Task<CustomerDto> UpdateAsync(Customer customer);
    Task DeleteAsync(Guid customerId);
}