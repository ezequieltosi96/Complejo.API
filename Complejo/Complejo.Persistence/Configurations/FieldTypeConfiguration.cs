using Complejo.Domain.Entities;
using Complejo.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Complejo.Persistence.Configurations
{
    public class FieldTypeConfiguration : BaseConfiguration<FieldType>
    {
        public FieldTypeConfiguration() : base("DEF_FieldType")
        {
        }

        public override void Configure(EntityTypeBuilder<FieldType> builder)
        {
            base.Configure(builder);

            GenericConfiguration.ConfigureDescription(builder);

            builder.Property(ft => ft.IdFieldTypeGroup)
                .HasColumnName("IdFieldTypeGroup")
                .IsRequired();
                
        }
    }
}
