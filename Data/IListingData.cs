using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.ListingViewModels;
using ecomFront.Models.SearchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public interface IListingData
    {
        public List<ListingToDataTable> GetListingToDataTable(int ExecutionId, string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount);
    }
}
