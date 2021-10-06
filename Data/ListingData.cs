using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.ListingViewModels;
using ecomFront.Models.SearchViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public class ListingData : IListingData
    {
        private ApplicationDbContext _contextModel;
        private DBFirstDbContext _contextDbFirst;

        public ListingData(DBFirstDbContext contextDbFirst,
                          ApplicationDbContext contextModel)
        {
            _contextDbFirst = contextDbFirst;
            _contextModel = contextModel;
        }


        public List<ListingToDataTable> GetListingToDataTable(int ExecutionId, string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            // the example datatable used is not supporting multi column ordering
            // so we only need get the column order from the first column passed to us.        
            //var whereClause = BuildDynamicWhereClause(Db, searchBy);

            var result = _contextDbFirst.Listings.Include(l => l.Category)
                           .Where(l => l.ExecutionId == ExecutionId && l.Elegible == 1)
                           .Select(l => new ListingToDataTable
                           {
                               IdMl = l.IdMl,
                               Category = l.Category,
                               DateCreated = l.DateCreated,
                               ListingCondition = l.ListingCondition,
                               ListingTypeId = l.ListingTypeId,
                               Price = l.Price,
                               Permalink = l.Permalink,
                               SellerIdMl = l.SellerIdMl,
                               SoldQuantity= l.SoldQuantity,
                               Thumbnail = l.Thumbnail, 
                               Title = l.Title,
                               TotalQuestions = l.TotalQuestions,
                               VisitsQuantity = l.VisitsQuantity,
                               ReviewsQuantity = l.ReviewsQuantity
                           })
                           .OrderByDescending(l => l.SoldQuantity) // have to give a default order when skipping .. so use the PK
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            filteredResultsCount = _contextDbFirst.Listings.Where(l => l.ExecutionId == ExecutionId && l.Elegible == 1).Count();
            totalResultsCount = filteredResultsCount;

            return result;
        }

        public List<ListingToDataTable> GetListingByCondition(int ExecutionId, int searchBy)
        {

            var result = _contextDbFirst.Listings.Include(l => l.Category)
                           .Where(l => l.ExecutionId == ExecutionId && l.Elegible == 1)
                           .Select(l => new ListingToDataTable
                           {
                               IdMl = l.IdMl,
                               Category = l.Category,
                               DateCreated = l.DateCreated,
                               ListingCondition = l.ListingCondition,
                               ListingTypeId = l.ListingTypeId,
                               Price = l.Price,
                               Permalink = l.Permalink,
                               SellerIdMl = l.SellerIdMl,
                               SoldQuantity = l.SoldQuantity,
                               Thumbnail = l.Thumbnail,
                               Title = l.Title,
                               TotalQuestions = l.TotalQuestions,
                               VisitsQuantity = l.VisitsQuantity,
                               ReviewsQuantity = l.ReviewsQuantity
                           })
                           .OrderByDescending(l => l.SoldQuantity) 
                           .ToList();

            return result;
        }
    }
}
