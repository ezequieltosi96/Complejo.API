using Complejo.Domain.Entities;
using Complejo.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Complejo.Persistence.Configurations
{
    public class FieldStatusConfiguration : BaseConfiguration<FieldStatus>
    {
        public FieldStatusConfiguration() : base("DEF_FieldStatus")
        {
        }

        public override void Configure(EntityTypeBuilder<FieldStatus> builder)
        {
            base.Configure(builder);

            GenericConfiguration.ConfigureDescription(builder);

            builder.Property(ft => ft.IdFieldStatusGroup)
                .HasColumnName("IdFieldStatusGroup")
                .IsRequired();
        }
    }
}
