using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class DBFirstDbContext : DbContext
    {

        public DBFirstDbContext(DbContextOptions<DBFirstDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auth> Auths { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryAttribute> CategoryAttributes { get; set; }
        public virtual DbSet<CategoryAttributeValue> CategoryAttributeValues { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CriteriaAttribute> CriteriaAttributes { get; set; }
        public virtual DbSet<Criterion> Criteria { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Execution> Executions { get; set; }
        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<Search> Searches { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=MySQLConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Auth>(entity =>
            {
                entity.ToTable("auth");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("access_token");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("client_id");

                entity.Property(e => e.ClientSecret)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("client_secret");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("last_updated");

                entity.Property(e => e.TokenType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("token_type");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategoryml)
                    .HasName("PRIMARY");

                entity.ToTable("category");

                entity.Property(e => e.IdCategoryml).HasColumnName("id_categoryml");

                entity.Property(e => e.FechaUltimaActualizacion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("fecha_ultima_actualizacion");

                entity.Property(e => e.IdCategoryPadre)
                    .HasMaxLength(255)
                    .HasColumnName("id_category_padre");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Linkml)
                    .HasMaxLength(255)
                    .HasColumnName("linkml");

                entity.Property(e => e.Nameml)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("nameml");

                entity.Property(e => e.Pictureml)
                    .HasMaxLength(255)
                    .HasColumnName("pictureml");

                entity.Property(e => e.TotalItemsml).HasColumnName("total_itemsml");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<CategoryAttribute>(entity =>
            {
                entity.HasKey(e => e.IdCategoryAttribute)
                    .HasName("PRIMARY");

                entity.ToTable("category_attributes");

                entity.HasIndex(e => e.CategoryId, "FK_f7naqg5qirisgeu2t617ttdmt");

                entity.Property(e => e.IdCategoryAttribute).HasColumnName("id_category_attribute");

                entity.Property(e => e.AttributeGroupIdml)
                    .HasMaxLength(255)
                    .HasColumnName("attribute_group_idml");

                entity.Property(e => e.AttributeGroupNameml)
                    .HasMaxLength(255)
                    .HasColumnName("attribute_group_nameml");

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasColumnName("category_id");

                entity.Property(e => e.IdAttributeml)
                    .HasMaxLength(255)
                    .HasColumnName("id_attributeml");

                entity.Property(e => e.Nameml)
                    .HasMaxLength(255)
                    .HasColumnName("nameml");

                entity.Property(e => e.ValueTypeml)
                    .HasMaxLength(255)
                    .HasColumnName("value_typeml");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryAttributes)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_f7naqg5qirisgeu2t617ttdmt");
            });

            modelBuilder.Entity<CategoryAttributeValue>(entity =>
            {
                entity.HasKey(e => e.IdCategoryAttributeValue)
                    .HasName("PRIMARY");

                entity.ToTable("category_attribute_values");

                entity.HasIndex(e => e.CategoryAttributeId, "FK_lhewwqb2xaq3fi4249a16v6in");

                entity.Property(e => e.IdCategoryAttributeValue).HasColumnName("id_category_attribute_value");

                entity.Property(e => e.CategoryAttributeId).HasColumnName("category_attribute_id");

                entity.Property(e => e.IdValueml)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("id_valueml");

                entity.Property(e => e.Nameml)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("nameml");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.CategoryAttribute)
                    .WithMany(p => p.CategoryAttributeValues)
                    .HasForeignKey(d => d.CategoryAttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lhewwqb2xaq3fi4249a16v6in");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.IdCity)
                    .HasName("PRIMARY");

                entity.ToTable("city");

                entity.HasIndex(e => e.StateId, "FK_ogqc1b0omhdvgo6vojoj95hv7");

                entity.Property(e => e.IdCity).HasColumnName("id_city");

                entity.Property(e => e.IdCityGob)
                    .HasMaxLength(255)
                    .HasColumnName("id_city_gob");

                entity.Property(e => e.Latitud).HasColumnName("latitud");

                entity.Property(e => e.Longitud).HasColumnName("longitud");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_ogqc1b0omhdvgo6vojoj95hv7");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IdCountry)
                    .HasName("PRIMARY");

                entity.ToTable("country");

                entity.Property(e => e.IdCountry).HasColumnName("id_country");

                entity.Property(e => e.Currency)
                    .HasMaxLength(255)
                    .HasColumnName("currency");

                entity.Property(e => e.Latitud).HasColumnName("latitud");

                entity.Property(e => e.Longitud).HasColumnName("longitud");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<CriteriaAttribute>(entity =>
            {
                entity.HasKey(e => e.IdCriteriaAttribute)
                    .HasName("PRIMARY");

                entity.ToTable("criteria_attributes");

                entity.HasIndex(e => e.CriteriaId, "FK_hagiuai14q0ealdyv0o2ot4uh");

                entity.Property(e => e.IdCriteriaAttribute).HasColumnName("id_criteria_attribute");

                entity.Property(e => e.CriteriaId).HasColumnName("criteria_id");

                entity.Property(e => e.IdAttributeValueml)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("id_attribute_valueml");

                entity.Property(e => e.IdAttributeml)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("id_attributeml");

                entity.Property(e => e.NameAttributeValueml)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name_attribute_valueml");

                entity.Property(e => e.NameAttributeml)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name_attributeml");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.Criteria)
                    .WithMany(p => p.CriteriaAttributes)
                    .HasForeignKey(d => d.CriteriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_hagiuai14q0ealdyv0o2ot4uh");
            });

            modelBuilder.Entity<Criterion>(entity =>
            {
                entity.HasKey(e => e.IdCriteria)
                    .HasName("PRIMARY");

                entity.ToTable("criteria");

                entity.HasIndex(e => e.SearchId, "FK_ilujai0v9b9t51ves787q0c14");

                entity.HasIndex(e => e.CategoryId, "FK_lpl9nnvgafr7d8q1oicgxii49");

                entity.Property(e => e.IdCriteria).HasColumnName("id_criteria");

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasColumnName("category_id");

                entity.Property(e => e.ItemCondition)
                    .HasMaxLength(255)
                    .HasColumnName("item_condition");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.RelatedLink)
                    .HasMaxLength(255)
                    .HasColumnName("related_link");

                entity.Property(e => e.SearchCriteria)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("search_criteria");

                entity.Property(e => e.SearchId).HasColumnName("search_id");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Criteria)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lpl9nnvgafr7d8q1oicgxii49");

                entity.HasOne(d => d.Search)
                    .WithMany(p => p.Criteria)
                    .HasForeignKey(d => d.SearchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ilujai0v9b9t51ves787q0c14");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.IdEvent)
                    .HasName("PRIMARY");

                entity.ToTable("events");

                entity.Property(e => e.IdEvent).HasColumnName("id_event");

                entity.Property(e => e.Estado)
                    .HasColumnType("bit(1)")
                    .HasColumnName("estado");

                entity.Property(e => e.FechaDesde)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_desde");

                entity.Property(e => e.FechaHasta)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_hasta");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<Execution>(entity =>
            {
                entity.HasKey(e => e.IdExecution)
                    .HasName("PRIMARY");

                entity.ToTable("executions");

                entity.HasIndex(e => e.SearchId, "FK_n99e6gu7gdsmy7yotcxs45out");

                entity.Property(e => e.IdExecution).HasColumnName("id_execution");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.DateExecuted)
                    .HasColumnType("datetime")
                    .HasColumnName("date_executed");

                entity.Property(e => e.DateFinished)
                    .HasColumnType("datetime")
                    .HasColumnName("date_finished");

                entity.Property(e => e.ErrorDescription)
                    .HasMaxLength(255)
                    .HasColumnName("error_description");

                entity.Property(e => e.ExecutionStatus)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("execution_status");

                entity.Property(e => e.ListingQtty).HasColumnName("listing_qtty");

                entity.Property(e => e.SearchId).HasColumnName("search_id");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.Search)
                    .WithMany(p => p.Executions)
                    .HasForeignKey(d => d.SearchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_n99e6gu7gdsmy7yotcxs45out");
            });

            modelBuilder.Entity<Listing>(entity =>
            {
                entity.HasKey(e => new { e.IdMl, e.ExecutionId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("listing");

                entity.HasIndex(e => e.CriteriaId, "FK_a9acw8l293sshxgnqbgdvc6mj");

                entity.HasIndex(e => new { e.SellerIdMl, e.SellerExecutionId }, "FK_grestosbo0sopxlmjgjtxjk93");

                entity.HasIndex(e => e.CategoryId, "FK_phb706a6e0upg5f8ara08j96e");

                entity.HasIndex(e => e.ExecutionId, "FK_thrdq98o2f4gq74kq4gjsdqn8");

                entity.Property(e => e.IdMl).HasColumnName("id_ml");

                entity.Property(e => e.ExecutionId).HasColumnName("execution_id");

                entity.Property(e => e.AcceptsMercadopago)
                    .HasColumnType("bit(1)")
                    .HasColumnName("accepts_mercadopago");

                entity.Property(e => e.AvailableQuantity).HasColumnName("available_quantity");

                entity.Property(e => e.BuyingMode)
                    .HasMaxLength(255)
                    .HasColumnName("buying_mode");

                entity.Property(e => e.CatalogProductId)
                    .HasMaxLength(255)
                    .HasColumnName("catalog_product_id");

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasColumnName("category_id");

                entity.Property(e => e.CriteriaId).HasColumnName("criteria_id");

                entity.Property(e => e.CurrencyId)
                    .HasMaxLength(255)
                    .HasColumnName("currency_id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.DomainId)
                    .HasMaxLength(255)
                    .HasColumnName("domain_id");

                entity.Property(e => e.Elegible)
                    .HasColumnType("bit(1)")
                    .HasColumnName("elegible");

                entity.Property(e => e.ListingCondition)
                    .HasMaxLength(255)
                    .HasColumnName("listing_condition");

                entity.Property(e => e.ListingTypeId)
                    .HasMaxLength(255)
                    .HasColumnName("listing_type_id");

                entity.Property(e => e.OfficialStoreId).HasColumnName("official_store_id");

                entity.Property(e => e.OrderBackend).HasColumnName("order_backend");

                entity.Property(e => e.OriginalPrice).HasColumnName("original_price");

                entity.Property(e => e.Permalink)
                    .HasMaxLength(255)
                    .HasColumnName("permalink");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.RatingAverage).HasColumnName("rating_average");

                entity.Property(e => e.ReviewsQuantity).HasColumnName("reviews_quantity");

                entity.Property(e => e.SellerExecutionId).HasColumnName("seller_execution_id");

                entity.Property(e => e.SellerIdMl).HasColumnName("seller_id_ml");

                entity.Property(e => e.SiteId)
                    .HasMaxLength(255)
                    .HasColumnName("site_id");

                entity.Property(e => e.SoldQuantity).HasColumnName("sold_quantity");

                entity.Property(e => e.StopTime)
                    .HasColumnType("datetime")
                    .HasColumnName("stop_time");

                entity.Property(e => e.Tags)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("tags");

                entity.Property(e => e.Thumbnail)
                    .HasMaxLength(255)
                    .HasColumnName("thumbnail");

                entity.Property(e => e.ThumbnailId)
                    .HasMaxLength(255)
                    .HasColumnName("thumbnail_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.TotalQuestions).HasColumnName("total_questions");

                entity.Property(e => e.UseThumbnailId)
                    .HasColumnType("bit(1)")
                    .HasColumnName("use_thumbnail_id");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.Property(e => e.VisitsQuantity).HasColumnName("visits_quantity");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_phb706a6e0upg5f8ara08j96e");

                entity.HasOne(d => d.Criteria)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.CriteriaId)
                    .HasConstraintName("FK_a9acw8l293sshxgnqbgdvc6mj");

                entity.HasOne(d => d.Execution)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.ExecutionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_thrdq98o2f4gq74kq4gjsdqn8");
            });

            modelBuilder.Entity<Search>(entity =>
            {
                entity.HasKey(e => e.IdSearch)
                    .HasName("PRIMARY");

                entity.ToTable("searches");

                entity.HasIndex(e => e.UserId, "FK_2ef0djn72ytqoh6k0c2f10w49");

                entity.Property(e => e.IdSearch).HasColumnName("id_search");

                entity.Property(e => e.CatalogProductIdml)
                    .HasMaxLength(255)
                    .HasColumnName("catalog_product_idml");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.ListingPermalink)
                    .HasMaxLength(255)
                    .HasColumnName("listing_permalink");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.SearchType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("search_type");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.IdState)
                    .HasName("PRIMARY");

                entity.ToTable("state");

                entity.HasIndex(e => e.CountryId, "FK_lxoqjm8644epv72af3k3jpalx");

                entity.Property(e => e.IdState).HasColumnName("id_state");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.IdGob)
                    .HasMaxLength(255)
                    .HasColumnName("id_gob");

                entity.Property(e => e.Latitud).HasColumnName("latitud");

                entity.Property(e => e.Longitud).HasColumnName("longitud");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_lxoqjm8644epv72af3k3jpalx");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
