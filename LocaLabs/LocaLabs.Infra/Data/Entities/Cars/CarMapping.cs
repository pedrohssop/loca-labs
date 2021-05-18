using LocaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocaLabs.Infra.Data.Entities.Cars
{
    class CarMapping : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.BrandId);
            builder.Property(p => p.HourValue);
            builder.Property(p => p.TrunkVolume);
            builder.Property(p => p.Plate).HasMaxLength(7);

            builder.Property(p => p.Fuel).HasConversion<int>();
            builder.Property(p => p.Category).HasConversion<int>();
            builder.Property(p => p.CreatedAt).HasColumnName("created_at");

            builder.HasIndex(i => new
            {
                i.Plate
            })
            .IsUnique();

            builder.Ignore(i => i.IsNew);
        }
    }
}
