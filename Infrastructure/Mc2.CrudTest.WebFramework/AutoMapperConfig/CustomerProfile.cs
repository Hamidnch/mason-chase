using AutoMapper;
using Mc2.CrudTest.Application.Cqrs.Customers.Dtos;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.WebFramework.Models;

namespace Mc2.CrudTest.WebFramework.AutoMapperConfig
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            //CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<CustomerModel, CustomerDto>().ReverseMap();
        }
    }
}
