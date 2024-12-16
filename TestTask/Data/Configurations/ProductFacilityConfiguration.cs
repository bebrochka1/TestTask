using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Data.Models;

namespace TestTask.Data.Configurations
{
    public class ProductFacilityConfiguration : IEntityTypeConfiguration<ProductFacility>
    {
        public void Configure(EntityTypeBuilder<ProductFacility> builder)
        {
            builder.HasKey(p => p.Code);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Area)
                .IsRequired();

            builder.HasData(
                new ProductFacility
                {
                    Code = "p001",
                    Name = "ProductA",
                    Area = 100
                },
                new ProductFacility
                {
                    Code = "p002",
                    Name = "ProductB",
                    Area = 75
                },
                new ProductFacility
                {
                    Code = "p003",
                    Name = "ProductC",
                    Area = 50
                },
                new ProductFacility
                {
                    Code = "p004",
                    Name = "ProductD",
                    Area = 25
                }
                );

            builder.ToTable("Product_Facilities");
        }
    }
}
