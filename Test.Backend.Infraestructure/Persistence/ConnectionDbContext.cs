

namespace Test.Backend.Infraestructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Test.Backend.Domain;
    using Test.Backend.Domain.Common;
    public class ConnectionDbContext : DbContext
    {
        public ConnectionDbContext(DbContextOptions<ConnectionDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Products> Products { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreateBy = "system";
                        entry.Entity.Status = true;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        entry.Entity.UpdateBy = "system";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Products>(entity =>
        //    {
        //        entity.ToTable("Products");
        //        entity.HasKey(e => e.ProductId);
        //        entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
        //        entity.Property(e => e.Description).HasMaxLength(250).IsRequired();
        //        entity.Property(e => e.Stock).IsRequired();
        //        entity.Property(e => e.Price);
        //        entity.Property(e => e.CreateBy).HasMaxLength(50);
        //        entity.Property(e => e.CreatedDate).HasColumnType("datetime");
        //        entity.Property(e => e.UpdateBy).HasMaxLength(50);
        //        entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        //    });
        //}
    }
}
