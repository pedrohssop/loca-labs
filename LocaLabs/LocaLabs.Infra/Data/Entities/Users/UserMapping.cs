using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocaLabs.Infra.Data.Entities.Users
{
    internal class UserMapping : IEntityTypeConfiguration<Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(i => new
            {
                i.Login,
                i.Password
            })
            .IsUnique();

            builder.Property(c => c.Login)
                   .HasMaxLength(50)
                   .HasColumnName("login")
                   .IsRequired();

            builder.Property(c => c.Password)
                   .HasMaxLength(200)
                   .HasColumnName("password")
                   .IsRequired();

            builder.Property(c => c.Type)
                   .HasMaxLength(200)
                   .HasColumnName("type")
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(p => p.CreatedAt)
                   .HasColumnName("created_at");

            builder.Ignore(u => u.IsNew);
        }
    }
}