using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CustomerMicroService.Domain.Entities;

namespace CustomerMicroService.Data.Mapping
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.LastName).IsRequired();

            builder.Property(c => c.Cpf)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasConversion<string>(v => v, v => v)
                .IsRequired();

            builder.HasOne(x => x.Address);


            builder.Property(c => c.ModifiedIn).ValueGeneratedOnUpdate();

            builder.Property(c => c.RowVersion).IsRowVersion();
        }
    }
}
// modelBuilder.Entity<Customer>()
//     .ToTable("Customer")
//      .HasKey(c => c.CustomerId);

// modelBuilder.Entity<Customer>()
//     .HasOne(c => c.Address);

// modelBuilder.Entity<Customer>()
//.Property(c => c.Cpf)
//    .HasMaxLength(11)
//     .IsUnicode(false)
//     .HasConversion<string>(v => v, v => v)
//     .IsRequired();

// modelBuilder.Entity<Customer>()
//     .Property(c => c.CreatedAt).ValueGeneratedOnAdd();

// modelBuilder.Entity<Customer>()
//     .Property(c => c.ModifiedIn).ValueGeneratedOnUpdate();

// modelBuilder.Entity<Address>()
//     .ToTable("Address")
//     .HasKey(a => a.AddressId);
