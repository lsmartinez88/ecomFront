using ecomFront.Common;
using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.ListingViewModels;
using ecomFront.Models.PricingViewModels;
using ecomFront.Models.SalesViewModels;
using ecomFront.Models.SellerViewModel;
using ecomFront.Models.SellerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ecomFront.Controllers
{
    [Authorize]
    public class ListingController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AnalisisController> _logger;
        private ISearchData _searchData;
        private IListingData _listingData;

        public ListingController(UserManager<ApplicationUser> userManager,
                                ILogger<AnalisisController> logger,
                                ISearchData searchData,
                                IListingData listingData)
        {
            _userManager = userManager;
            _searchData = searchData;
            _logger = logger;
            _listingData = listingData;
        }

        public IActionResult ListingList(int? executionId)
        {
            var listingListViewModel = new ListingListViewModel
            {
                Execution = _searchData.GetExecution(executionId)
            };
            return View(listingListViewModel);
        }

        [HttpPost]
        public JsonResult ListingInfoDataTable(DataTableAjaxPostModel model)
        {
            // action inside a standard controller
            int filteredResultsCount;
            int totalResultsCount;
            var res = GetListingList(model, out filteredResultsCount, out totalResultsCount);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = res
            });
        }

        public IList<ListingToDataTable> GetListingList(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                //sortDir = model.order[0].dir.ToLower() == "asc";
            }

            // search the dbase taking into consideration table sorting and paging
            var result = _listingData.GetListingToDataTable(model.executionId, searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<ListingToDataTable>();
            }
            return result;
        }
    }

    
}
