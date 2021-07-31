using ecomFront.Models.DbFirstModels;
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
    }
}
