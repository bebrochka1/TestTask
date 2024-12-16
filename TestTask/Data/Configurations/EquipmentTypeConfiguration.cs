using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Data.Models;

namespace TestTask.Data.Configurations
{
    public class EquipmentTypeConfiguration : IEntityTypeConfiguration<EquipmentType>
    {
        public void Configure(EntityTypeBuilder<EquipmentType> builder)
        {
            builder.HasKey(e => e.Code);

            builder.Property(e => e.Code)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Area)
                .IsRequired();

            builder.HasData(
                new EquipmentType
                {
                    Code = "e001",
                    Name = "EquipmentA",
                    Area = 20
                },
                new EquipmentType
                {
                    Code = "e002",
                    Name = "EquipmentB",
                    Area = 15
                },
                new EquipmentType
                {
                    Code = "e003",
                    Name = "EquipmentC",
                    Area = 5
                },
                new EquipmentType
                {
                    Code = "e004",
                    Name = "EquipmentD",
                    Area = 40
                }
                );
            builder.ToTable("Equipment_Types");
        }
    }
}
