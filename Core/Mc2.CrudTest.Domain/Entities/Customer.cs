using NetDevPack.Domain;

namespace Mc2.CrudTest.Domain.Entities
{
    /// <summary>
    /// Customer entity
    /// </summary>
    public class Customer : Entity, IAggregateRoot
    {
        public Customer(Guid id, string? firstname, string? lastname, DateTime dateOfBirth,
            string? phoneNumber, string? email, string? bankAccountNumber)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
        }

        //Empty constructor For EF purpose
        protected Customer() { }

        /// <summary>
        /// First name of customer
        /// </summary>
        public string? Firstname { get; private set; }

        /// <summary>
        /// Last name of customer
        /// </summary>
        public string? Lastname { get; private set; }

        /// <summary>
        /// Date of birth of customer
        /// </summary>
        public DateTime DateOfBirth { get; private set; }

        /// <summary>
        /// Phone number of customer
        /// </summary>
        public string? PhoneNumber { get; private set; }

        /// <summary>
        /// Email of customer
        /// </summary>
        public string? Email { get; private set; }
        /// <summary>
        /// Bank account number of customer
        /// </summary>
        public string? BankAccountNumber { get; private set; }
    }
}