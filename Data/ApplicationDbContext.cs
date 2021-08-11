using ecomFront.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(300);

        }

        public virtual DbSet<ListingGrouping> ListingGrouping { get; set; }
        public virtual DbSet<ItemGrouping> ItemGrouping { get; set; }
        public virtual DbSet<ListingIndicador> ListingIndicador { get; set; }
        public virtual DbSet<PriceRangeGrouping> PriceRangeGrouping { get; set; }
        public virtual DbSet<AveragePricePerDay> AveragePricePerDay { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ListingGrouping>()
                .HasKey(c => new { c.CriteriaId, c.ExecutionId, c.GroupingType , c.ItemGroupingId });

            modelBuilder.Entity<ItemGrouping>()
            .HasAlternateKey(a => new { a.GroupingType, a.IdGrouping });

            modelBuilder.Entity<ListingIndicador>()
            .HasKey(a => new { a.CriteriaId, a.ExecutionId, a.TipoIndicador });

            modelBuilder.Entity<PriceRangeGrouping>()
            .HasKey(a => new { a.CriteriaId, a.ExecutionId, a.RangoDesde, a.RangoHasta, a.GroupingType, a.ItemGroupingId});

            modelBuilder.Entity<AveragePricePerDay>()
            .HasKey(a => new { a.CriteriaId, a.ExecutionId, a.Fecha });
        }
    }
}
