using Complejo.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Complejo.Persistence.Configurations.Base
{
    public class AuditBaseConfiguration<T> : BaseConfiguration<T> where T : AuditEntityBase
    {
        public AuditBaseConfiguration(string tableName) : base(tableName)
        {
        }

        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CreatedBy)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.LastUpdatedBy)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(x => x.LastUpdatedDate)
                .HasColumnType("datetime");
        }
    }
}
