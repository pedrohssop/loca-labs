using LocaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocaLabs.Infra.Data.Entities.Cars
{
    public class CarBrandMapping : IEntityTypeConfiguration<CarBrand>
    {
        public void Configure(EntityTypeBuilder<CarBrand> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Year);
            builder.Property(p => p.Model).HasMaxLength(200);
            builder.Property(p => p.Brand).HasMaxLength(200);
            builder.Property(p => p.CreatedAt).HasColumnName("created_at");

            builder.Ignore(i => i.IsNew);
        }
    }
}
