using Mc2.CrudTest.Application.Cqrs.Customers.Dtos;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Services;

public interface ICustomerService
{
    Task<ResponseCustomerDto> GetAllAsync(RequestCustomerDto dto);
    Task<CustomerDto> GetByIdAsync(int id);
    Task<CustomerDto> CreateAsync(CustomerDto customerDto);
    Task<CustomerDto> UpdateAsync(CustomerDto customerDto);
    Task DeleteAsync(CustomerDto customerDto);
}