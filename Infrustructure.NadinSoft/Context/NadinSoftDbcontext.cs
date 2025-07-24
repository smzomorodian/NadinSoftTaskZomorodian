using Domain.NadinSoft.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.NadinSoft.Context
{
    public class NadinSoftDbcontext : IdentityDbContext<ApplicationUser>
    {
        protected NadinSoftDbcontext()
        {
        }
        public NadinSoftDbcontext(DbContextOptions<NadinSoftDbcontext> options)
            : base(options)
        {
        }

        public DbSet<Product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.ManufacturePhone)
                .IsRequired().HasMaxLength(20);

                entity.Property(p => p.ManufactureEmail)
                .IsRequired().HasMaxLength(50);

                entity.Property(p => p.Name)
                .IsRequired().HasMaxLength(30);

                entity.HasIndex(p => new { p.ManufactureEmail, p.ProduceDate })
                .IsUnique();

                entity.HasOne<ApplicationUser>()
                    .WithMany(u => u.Products)
                    .HasForeignKey(p => p.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(p => p.Nationalcode)
                .IsRequired().HasMaxLength(10);
            });
        }
    }
}
