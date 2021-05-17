using LocaLabs.Domain.Entities;
using LocaLabs.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocaLabs.Infra.Data.Entities.Clients
{
    internal class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Ignore(p => p.IsNew);
            builder.Property(p => p.Dob);
            builder.Property(p => p.City);
            builder.Property(p => p.Complement);
            builder.Property(p => p.CreatedAt);

            builder.Property(p => p.Street)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(p => p.Number)
                   .IsRequired();

            builder.Property(p => p.Cpf)
                   .IsRequired()
                   .HasConversion(i => i.Unformated, o => (Cpf)o);

            builder.Property(p => p.State)
                   .IsRequired()
                   .HasConversion(i => i.Initials, o => (State)o);

            builder.Property(p => p.Cep)
                   .HasConversion(i => i.Unformated, o => (Cep)o);
        }
    }
}