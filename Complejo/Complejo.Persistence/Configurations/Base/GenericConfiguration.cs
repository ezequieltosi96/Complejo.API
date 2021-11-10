using Complejo.Domain.Common;
using Complejo.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Complejo.Persistence.Configurations.Base
{
    public static class GenericConfiguration
    {
        public static void ConfigureDescription<T>(EntityTypeBuilder<T> builder, int lenght = 100, bool required = true) where T : EntityBase, IDescription
        {
            builder.Property(x => x.Description)
                .IsRequired(required)
                .HasColumnType("varchar")
                .HasMaxLength(lenght);

            builder.Property(x => x.DescriptionSearch)
                .IsRequired(required)
                .HasColumnType("varchar")
                .HasMaxLength(lenght)
                .HasComputedColumnSql("upper([Description])");
        }
    }
}
