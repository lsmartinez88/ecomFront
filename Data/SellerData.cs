using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.SearchViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public class SellerData : ISellerData
    {
        private ApplicationDbContext _contextModel;
        private DBFirstDbContext _contextDbFirst;

        public SellerData(DBFirstDbContext contextDbFirst,
                          ApplicationDbContext contextModel)
        {
            _contextDbFirst = contextDbFirst;
            _contextModel = contextModel;
        }

        public Execution GetExecution(int? executionId)
        {
            return _contextDbFirst.Executions.FirstOrDefault(e => e.IdExecution == executionId);
        }

        public List<Criterion> GetFullCriteriasBySearchId(int? SearchId)
        {
            return _contextDbFirst.Criteria
                    .Include(c => c.Category)
                    .Include(c => c.CriteriaAttributes)
                    .AsSplitQuery()
                    .Where(c => c.SearchId == SearchId)
                    .ToList();
        }

        public List<Search> GetFullSearches(string UserId)
        {
            return _contextDbFirst.Searches
                        .Include(s => s.Criteria)
                        .ThenInclude(cri => cri.Category)
                        .Include(s => s.Criteria)
                        .ThenInclude(c => c.CriteriaAttributes)
                        .Include(s => s.Executions)
                        .Where(s => s.UserId == UserId)
                        .AsSplitQuery()
                        .ToList();
        }


        public Search GetSearch(int? searchId)
        {
            return _contextDbFirst.Searches
                .FirstOrDefault(s => s.IdSearch == searchId);
        }

        public List<TopSellers> GetSellersListByExecution(int? executionId)
        {
            return _contextModel.TopSellers.Where(ts => ts.ExecutionId == (int)executionId).ToList();
        }

        public List<TopSellers> GetSalesMonthSeller(int? executionId, String sellerId)
        {
            return _contextModel.TopSellers.Where(ts => ts.ExecutionId == (int)executionId && ts.SellerId.Equals(sellerId) && ts.ParameterName.StartsWith("Cantidad_")).OrderBy(ts => ts.ParameterName).ToList();
        }

        public List<TopSellers> GetPriceMonthSeller(int? executionId, String sellerId)
        {
            return _contextModel.TopSellers.Where(ts => ts.ExecutionId == (int)executionId && ts.SellerId.Equals(sellerId) && ts.ParameterName.StartsWith("Precio_")).OrderBy(ts => ts.ParameterName).ToList();
        }
    }
}
