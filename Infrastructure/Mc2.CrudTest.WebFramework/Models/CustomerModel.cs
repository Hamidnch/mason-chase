using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.WebFramework.Models
{
    /// <summary>
    /// Customer entity
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// Customer identifier
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// First name of customer
        /// </summary>
        [Required(ErrorMessage = "Please enter your first name")]
        public string? Firstname { get; set; }

        /// <summary>
        /// Last name of customer
        /// </summary>
        [Required(ErrorMessage = "Please enter your last name")]
        public string? Lastname { get; set; }

        /// <summary>
        /// Date of birth of customer
        /// </summary>
        [Required(ErrorMessage = "Please enter date of your birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Phone number of customer
        /// </summary>
        [Phone]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email of customer
        /// </summary>
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Bank account number of customer
        /// </summary>
        [RegularExpression("((\\d{4})-){3}\\d{4}", ErrorMessage = "Invalid account number")]
        public string? BankAccountNumber { get; set; }
    }
}
