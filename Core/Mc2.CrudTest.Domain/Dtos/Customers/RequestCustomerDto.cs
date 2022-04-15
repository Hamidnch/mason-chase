namespace Mc2.CrudTest.Domain.Dtos.Customers
{
    public class RequestCustomerDto
    {
        public string? SearchText { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
