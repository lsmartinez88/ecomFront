using ecomFront.Models.DbFirstModels;
using ecomFront.Models.SearchViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public interface ISearchData
    {
        List<Search> GetFullSearches(string UserId);
        Search GetSearch(int? searchId);
        Execution GetExecution(int? executionId);

        List<Criterion> GetFullCriteriasBySearchId(int ?SearchId);
        Category GetCategoryById(string idCategory);

        List<Execution> GetFullExecutionsBySearchId(int? SearchId);

        Search SaveSearch(Search search);
        Criterion SaveCriteria(Criterion search);

        CriteriaAttribute SaveCriteriaAttribute(CriteriaAttribute criteriaAttribute);

        List<SalesQuantityMap> GetSalesMap(int ExecutionId, SalesType tipo);
        List<SalesQuantityMap> GetSalesMapCriteria(int ExecutionId, int CriteriaId, SalesType tipo);
        
    }
}
