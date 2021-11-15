using Complejo.Domain.Entities;
using Complejo.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Complejo.Persistence.Configurations
{
    public class ClientConfiguration : BaseConfiguration<Client>
    {
        public ClientConfiguration() : base("Client")
        {
        }

        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.FullName)
                   .HasColumnType("varchar")
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(c => c.FullNameSearch)
                   .HasColumnType("varchar")
                   .HasMaxLength(250)
                   .IsRequired()
                   .HasComputedColumnSql($"upper([{nameof(Client.FullName)}])");

            builder.Property(c => c.PhoneNumber)
                   .HasColumnType("varchar")
                   .HasMaxLength(13)
                   .IsRequired();

            builder.Property(c => c.Dni)
                   .HasColumnType("varchar")
                   .HasMaxLength(8)
                   .IsRequired();
        }
    }
}
