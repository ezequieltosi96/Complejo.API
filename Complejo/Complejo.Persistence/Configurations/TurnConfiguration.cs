using Complejo.Domain.Entities;
using Complejo.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Complejo.Persistence.Configurations
{
    class TurnConfiguration : AuditBaseConfiguration<Turn>
    {
        public TurnConfiguration() : base("Turn")
        {
        }

        public override void Configure(EntityTypeBuilder<Turn> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Date).HasColumnType("date").IsRequired();

            builder.Property(t => t.Code).HasColumnType("varchar").HasMaxLength(7).IsRequired();

            builder.Property(t => t.Time).HasColumnType("datetime").IsRequired();

            builder.HasOne(t => t.Field).WithMany().HasForeignKey(x => x.IdField);

            builder.HasOne(t => t.Client).WithMany().HasForeignKey(x => x.IdClient).IsRequired(false);
        }
    }
}
