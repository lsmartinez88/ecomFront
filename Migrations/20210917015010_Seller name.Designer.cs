// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ecomFront.Data;

namespace ecomFront.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210917015010_Seller name")]
    partial class Sellername
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ecomFront.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Empresa")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ecomFront.Models.AveragePricePerDay", b =>
                {
                    b.Property<int>("CriteriaId")
                        .HasColumnType("int");

                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CantidadVentas")
                        .HasColumnType("int");

                    b.Property<double>("PrecioMedio")
                        .HasColumnType("double");

                    b.HasKey("CriteriaId", "ExecutionId", "Fecha");

                    b.ToTable("front_average_price_per_day");
                });

            modelBuilder.Entity("ecomFront.Models.ItemGrouping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("GroupDescription")
                        .HasColumnType("longtext");

                    b.Property<string>("GroupingType")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("IdGrouping")
                        .HasColumnType("int");

                    b.Property<string>("Nameml")
                        .HasColumnType("longtext");

                    b.Property<string>("NamemlShort")
                        .HasColumnType("longtext");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("GroupingType", "IdGrouping");

                    b.ToTable("ItemGrouping");
                });

            modelBuilder.Entity("ecomFront.Models.ListingGrouping", b =>
                {
                    b.Property<int>("CriteriaId")
                        .HasColumnType("int");

                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<string>("GroupingType")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ItemGroupingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CriteriaId", "ExecutionId", "GroupingType", "ItemGroupingId");

                    b.HasIndex("ItemGroupingId");

                    b.ToTable("front_listing_group");
                });

            modelBuilder.Entity("ecomFront.Models.ListingIndicador", b =>
                {
                    b.Property<int>("CriteriaId")
                        .HasColumnType("int");

                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<string>("TipoIndicador")
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.HasKey("CriteriaId", "ExecutionId", "TipoIndicador");

                    b.ToTable("front_listing_indicador");
                });

            modelBuilder.Entity("ecomFront.Models.PriceRangeGrouping", b =>
                {
                    b.Property<int>("CriteriaId")
                        .HasColumnType("int");

                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<int>("RangoDesde")
                        .HasColumnType("int");

                    b.Property<int>("RangoHasta")
                        .HasColumnType("int");

                    b.Property<string>("GroupingType")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ItemGroupingId")
                        .HasColumnType("int");

                    b.Property<int>("CantidadPublicaciones")
                        .HasColumnType("int");

                    b.Property<int>("CantidadVentas")
                        .HasColumnType("int");

                    b.HasKey("CriteriaId", "ExecutionId", "RangoDesde", "RangoHasta", "GroupingType", "ItemGroupingId");

                    b.HasIndex("ItemGroupingId");

                    b.ToTable("front_price_range_grouping");
                });

            modelBuilder.Entity("ecomFront.Models.SalesPerCity", b =>
                {
                    b.Property<int>("CriteriaId")
                        .HasColumnType("int");

                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("State")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("CantidadCompras")
                        .HasColumnType("int");

                    b.Property<int>("CantidadVentas")
                        .HasColumnType("int");

                    b.Property<string>("Latitud")
                        .HasColumnType("longtext");

                    b.Property<string>("Longitud")
                        .HasColumnType("longtext");

                    b.HasKey("CriteriaId", "ExecutionId", "City", "State");

                    b.ToTable("front_sales_per_city");
                });

            modelBuilder.Entity("ecomFront.Models.TopSellersInfo", b =>
                {
                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<string>("SellerId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("CancelacionesHistorico")
                        .HasColumnType("int");

                    b.Property<double>("CancellationRate")
                        .HasColumnType("double");

                    b.Property<int>("CancellationValue")
                        .HasColumnType("int");

                    b.Property<int>("CantidadPreguntas")
                        .HasColumnType("int");

                    b.Property<int>("CantidadPublicaciones")
                        .HasColumnType("int");

                    b.Property<int>("CantidadReviews")
                        .HasColumnType("int");

                    b.Property<int>("CantidadVentas")
                        .HasColumnType("int");

                    b.Property<int>("CantidadVisitas")
                        .HasColumnType("int");

                    b.Property<double>("ClaimsRate")
                        .HasColumnType("double");

                    b.Property<int>("ClaimsValue")
                        .HasColumnType("int");

                    b.Property<double>("DelayedRate")
                        .HasColumnType("double");

                    b.Property<int>("DelayedValue")
                        .HasColumnType("int");

                    b.Property<int>("PosicionVendedor")
                        .HasColumnType("int");

                    b.Property<string>("PublicacionMayorVenta")
                        .HasColumnType("longtext");

                    b.Property<int>("PublicacionMayorVentaCantidad")
                        .HasColumnType("int");

                    b.Property<double>("RatingNegativo")
                        .HasColumnType("double");

                    b.Property<double>("RatingNeutral")
                        .HasColumnType("double");

                    b.Property<double>("RatingPositivo")
                        .HasColumnType("double");

                    b.Property<string>("SellerName")
                        .HasColumnType("longtext");

                    b.Property<string>("TipoVendedor")
                        .HasColumnType("longtext");

                    b.Property<string>("ZonaMayorVenta")
                        .HasColumnType("longtext");

                    b.Property<int>("ZonaMayorVentaCantidad")
                        .HasColumnType("int");

                    b.HasKey("ExecutionId", "SellerId");

                    b.ToTable("front_top_sellers_info");
                });

            modelBuilder.Entity("ecomFront.Models.TrendsTreemap", b =>
                {
                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<string>("TrendName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TrendPadre")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("CantidadPublicaciones")
                        .HasColumnType("int");

                    b.Property<int?>("CantidadVentas")
                        .HasColumnType("int");

                    b.Property<double?>("IndiceColor")
                        .HasColumnType("double");

                    b.Property<double?>("IndiceSize")
                        .HasColumnType("double");

                    b.Property<int>("PosicionTendencia")
                        .HasColumnType("int");

                    b.Property<double?>("VentasPorPublicacion")
                        .HasColumnType("double");

                    b.HasKey("ExecutionId", "TrendName", "TrendPadre");

                    b.ToTable("front_trends_treemap");
                });

            modelBuilder.Entity("ecomFront.Models.WordCloudGrouping", b =>
                {
                    b.Property<int>("CriteriaId")
                        .HasColumnType("int");

                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<string>("Palabra")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("CantidadPublicaciones")
                        .HasColumnType("int");

                    b.Property<int?>("CantidadVentas")
                        .HasColumnType("int");

                    b.Property<double?>("IndicadorOportunidad")
                        .HasColumnType("double");

                    b.HasKey("CriteriaId", "ExecutionId", "Palabra");

                    b.ToTable("front_word_cloud_grouping");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ecomFront.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ecomFront.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ecomFront.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ecomFront.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ecomFront.Models.ListingGrouping", b =>
                {
                    b.HasOne("ecomFront.Models.ItemGrouping", "ItemGrouping")
                        .WithMany("ListingGroupings")
                        .HasForeignKey("ItemGroupingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemGrouping");
                });

            modelBuilder.Entity("ecomFront.Models.PriceRangeGrouping", b =>
                {
                    b.HasOne("ecomFront.Models.ItemGrouping", "ItemGrouping")
                        .WithMany()
                        .HasForeignKey("ItemGroupingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemGrouping");
                });

            modelBuilder.Entity("ecomFront.Models.ItemGrouping", b =>
                {
                    b.Navigation("ListingGroupings");
                });
#pragma warning restore 612, 618
        }
    }
}
