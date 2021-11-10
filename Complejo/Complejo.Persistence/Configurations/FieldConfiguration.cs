using Complejo.Domain.Entities;
using Complejo.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Complejo.Persistence.Configurations
{
    public class FieldConfiguration : AuditBaseConfiguration<Field>
    {
        public FieldConfiguration() : base("Field")
        {
        }

        public override void Configure(EntityTypeBuilder<Field> builder)
        {
            base.Configure(builder);

            GenericConfiguration.ConfigureDescription(builder, 100);

            builder.HasOne(f => f.FieldStatus).WithMany().HasForeignKey(x => x.IdFieldStatus);

            builder.HasOne(f => f.FieldType).WithMany().HasForeignKey(x => x.IdFieldType);
        }
    }
}
