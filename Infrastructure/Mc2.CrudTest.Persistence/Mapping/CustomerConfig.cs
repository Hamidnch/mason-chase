using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Persistence.Mapping
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.HasIndex(x => new { x.Firstname, x.Lastname, x.DateOfBirth }).IsUnique().HasFilter(null);
            builder.HasIndex(x => x.Email).HasDatabaseName("IX_Mc2_Email").IsUnique();
            builder.Property(x => x.Firstname).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Lastname).HasMaxLength(150).IsRequired();
            builder.Property(x => x.PhoneNumber).HasConversion<ulong>();
            builder.Property(x => x.BankAccountNumber).HasMaxLength(50);

            builder.HasData(
                new Customer(
                    Guid.NewGuid(),
                    "Hamid",
                    "NCH",
                    new DateTime(1981, 8, 10),
                    "09124820700",
                    "Hamidnch2007@gmail.com",
                    "123456"),
                new Customer(
                    Guid.NewGuid(),
                    "Ali",
                    "Razavi",
                    new DateTime(2001, 5, 5),
                    "09123526532",
                    "RazaviAli@gmail.com",
                    "3251388")
                );
        }
    }
}
