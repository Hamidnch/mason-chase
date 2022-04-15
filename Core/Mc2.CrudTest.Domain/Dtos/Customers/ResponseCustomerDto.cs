namespace Mc2.CrudTest.Domain.Dtos.Customers
{
    public class ResponseCustomerDto
    {
        public IReadOnlyList<CustomerDto> CustomerDtos { get; init; }
        public int Rows { get; set; }
    }
}
