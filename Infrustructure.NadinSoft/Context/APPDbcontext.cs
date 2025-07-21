using Domain.NadinSoft.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.NadinSoft.Context
{
    public class APPDbcontext : DbContext
    {
        protected APPDbcontext()
        {
        }
        public APPDbcontext(DbContextOptions<APPDbcontext> options) : base(options)
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
                .IsRequired().HasMaxLength(100);

                entity.Property(p => p.Name)
                .IsRequired().HasMaxLength(200);

                entity.HasIndex(p => new { p.ManufactureEmail, p.ProduceDate })
                .IsUnique();


            });
        }
    }
}
