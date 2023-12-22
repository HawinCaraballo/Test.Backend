using Microsoft.EntityFrameworkCore;
using Test.Backend.Domain.Common;
using Test.Backend.Domain;

namespace Test.Backend.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=(local);Initial Catalog=Test_ProductDB;User Id=sa;Password=s3gur1d4d;Integrated Security=SSPI;TrustServerCertificate=True");
        //}


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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(250).IsRequired();
                entity.Property(e => e.Stock).IsRequired();
                entity.Property(e => e.Price);
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.UpdateBy).HasMaxLength(50);
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
                entity.Property(e => e.Status);
            });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Products> Products { get; set; }

    }
}
