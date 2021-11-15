using Complejo.Application.Interfaces.Security;
using Complejo.Domain.Common;
using Complejo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Complejo.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IUserContext userContext;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserContext userContext) : base(options)
        {
            this.userContext = userContext;
        }

        public DbSet<Field> Fields { get; set; }
        public DbSet<FieldStatus> FieldStatus { get; set; }
        public DbSet<FieldType> FieldTypes { get; set; }
        public DbSet<Turn> Turns { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // ---- for auditable entities ----
            foreach (var entry in ChangeTracker.Entries<AuditEntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = userContext != null && userContext.UserName != null ? userContext.UserName : "Cliente";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastUpdatedDate = DateTime.Now;
                        entry.Entity.LastUpdatedBy = userContext.UserName;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
