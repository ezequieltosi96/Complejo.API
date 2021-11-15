using Complejo.Domain.Entities;
using Complejo.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

            builder.HasData(
                new FieldType
                {
                    Id = Guid.NewGuid(),
                    Description = "Fútbol 5",
                    IdFieldTypeGroup = 1,
                },
                new FieldType
                {
                    Id = Guid.NewGuid(),
                    Description = "Fútbol 8",
                    IdFieldTypeGroup = 2,
                },
                new FieldType
                {
                    Id = Guid.NewGuid(),
                    Description = "Fútbol 11",
                    IdFieldTypeGroup = 3,
                });
        }
    }
}
