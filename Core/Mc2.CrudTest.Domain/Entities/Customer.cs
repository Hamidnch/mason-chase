namespace Mc2.CrudTest.Domain.Entities
{
    /// <summary>
    /// Customer entity
    /// </summary>
    public class Customer : BaseEntity
    {
        /// <summary>
        /// First name of customer
        /// </summary>
        public string? Firstname { get; set; }

        /// <summary>
        /// Last name of customer
        /// </summary>
        public string? Lastname { get; set; }

        /// <summary>
        /// Date of birth of customer
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Phone number of customer
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email of customer
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Bank account number of customer
        /// </summary>
        public string? BankAccountNumber { get; set; }
    }
}