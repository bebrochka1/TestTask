using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Data.Models;

namespace TestTask.Data.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.Property(c => c.EquipmentTypeCode)
                .IsRequired();

            builder.Property(c => c.ProductFacilityCode)
                .IsRequired();

            builder.HasOne(c => c.Equipment)
                .WithMany(e => e.Contracts)
                .HasForeignKey(c => c.EquipmentTypeCode);

            builder.HasOne(c => c.Product)
                .WithMany(p => p.Contracts)
                .HasForeignKey(c => c.ProductFacilityCode);

            builder.ToTable("Contracts");
        }
    }
}
