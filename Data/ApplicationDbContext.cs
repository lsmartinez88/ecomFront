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
        public virtual DbSet<SalesPerCity> SalesPerCity { get; set; }
        public virtual DbSet<WordCloudGrouping> WordCloudGrouping { get; set; }
        public virtual DbSet<TrendsTreemap> TrendsTreemap { get; set; }
        public virtual DbSet<BarChartOportunity> BarChartOportunity { get; set; }
        public virtual DbSet<TopSellers> TopSellers { get; set; }
        public virtual DbSet<MainDashboard> MainDashboard { get; set; }
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

            modelBuilder.Entity<SalesPerCity>()
            .HasKey(a => new { a.CriteriaId, a.ExecutionId, a.City, a.State });

            modelBuilder.Entity<WordCloudGrouping>()
           .HasKey(a => new { a.CriteriaId, a.ExecutionId, a.Palabra });

            modelBuilder.Entity<TrendsTreemap>()
           .HasKey(a => new { a.ExecutionId, a.TrendName, a.TrendPadre });

            modelBuilder.Entity<TopSellers>()
          .HasKey(a => new { a.ExecutionId, a.SellerId, a.ParameterName });

            modelBuilder.Entity<BarChartOportunity>()
         .HasKey(a => new { a.ExecutionId, a.Palabra });

            modelBuilder.Entity<MainDashboard>()
        .HasKey(a => new { a.UserId, a.SearchId, a.ParameterName });
        }
    }
}
