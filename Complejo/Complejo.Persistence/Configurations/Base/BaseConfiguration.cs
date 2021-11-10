using Complejo.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Complejo.Persistence.Configurations.Base
{
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        protected string TableName { get; }

        public BaseConfiguration(string tableName)
        {
            this.TableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            if(!string.IsNullOrEmpty(TableName))
            {
                builder.HasKey(x => x.Id);
                builder.ToTable(TableName);
            }

            builder.Property(x => x.Removed)
                .IsRequired()
                .HasColumnName("Removed")
                .HasColumnType("bit")
                .HasDefaultValueSql(@"0");
        }
    }
}
