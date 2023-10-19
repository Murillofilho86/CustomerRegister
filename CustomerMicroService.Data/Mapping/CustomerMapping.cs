using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CustomerMicroService.Domain.Entities;
using System.Reflection.Emit;

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
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasConversion<string>(v => v, v => v)
                .IsRequired();

            builder.Property(c => c.AddressId);

            builder.OwnsOne(x => x.Address, ca =>
            {
                ca.Property(x => x.City).HasColumnName("City").HasMaxLength(80).IsUnicode(false).IsRequired();
                ca.Property(x => x.Neighborhood).HasColumnName("Neighborhood").HasMaxLength(80).IsUnicode(false).IsRequired();
                ca.Property(x => x.State).HasColumnName("State").HasMaxLength(80).IsUnicode(false).IsRequired();
                ca.Property(x => x.Street).HasColumnName("Street").HasMaxLength(100).IsUnicode(false).IsRequired();
                ca.Property(x => x.ZipCode).HasColumnName("ZipCode").HasMaxLength(9).IsUnicode(false).IsRequired();
                ca.Property(x => x.Number).HasColumnName("Number").HasMaxLength(20).IsUnicode(false).IsRequired();
                ca.Property(x => x.Complement).HasColumnName("Complement").HasMaxLength(20).IsUnicode(false);

            });

            builder.Property(c => c.CreatedAt).ValueGeneratedOnAdd();
            builder.Property(c => c.ModifiedIn).ValueGeneratedOnUpdate();
        }
    }
}
