using System;
using System.Collections.Generic;

#nullable disable

namespace ecomFront.Models.DbFirstModels
{
    public partial class Listing
    {
        public long ExecutionId { get; set; }
        public string IdMl { get; set; }
        public long Version { get; set; }
        public ulong? AcceptsMercadopago { get; set; }
        public long? AvailableQuantity { get; set; }
        public string BuyingMode { get; set; }
        public string CatalogProductId { get; set; }
        public string CategoryId { get; set; }
        public long? CriteriaId { get; set; }
        public string CurrencyId { get; set; }
        public DateTime DateCreated { get; set; }
        public string DomainId { get; set; }
        public string ListingCondition { get; set; }
        public string ListingTypeId { get; set; }
        public long? OfficialStoreId { get; set; }
        public long? OrderBackend { get; set; }
        public double? OriginalPrice { get; set; }
        public string Permalink { get; set; }
        public double? Price { get; set; }
        public long SellerIdMl { get; set; }
        public string SiteId { get; set; }
        public long? SoldQuantity { get; set; }
        public DateTime? StopTime { get; set; }
        public string Tags { get; set; }
        public string Thumbnail { get; set; }
        public string ThumbnailId { get; set; }
        public string Title { get; set; }
        public ulong? UseThumbnailId { get; set; }
        public long? TotalQuestions { get; set; }
        public long? VisitsQuantity { get; set; }
        public long? ReviewsQuantity { get; set; }
        public double? RatingAverage { get; set; }
        public long SellerExecutionId { get; set; }
        public ulong Elegible { get; set; }

        public virtual Category Category { get; set; }
        public virtual Criterion Criteria { get; set; }
        public virtual Execution Execution { get; set; }
    }
}
