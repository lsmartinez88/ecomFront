using ecomFront.Models.DbFirstModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public class SearchData : ISearchData
    {
        private ApplicationDbContext _contextModel;
        private DBFirstDbContext _contextDbFirst;
        public SearchData(DBFirstDbContext contextDbFirst,
                          ApplicationDbContext contextModel)
        {
            _contextDbFirst = contextDbFirst;
            _contextModel = contextModel;
        }

        public Category GetCategoryById(string idCategory)
        {
            return _contextDbFirst.Categories.FirstOrDefault(c => c.IdCategoryml == idCategory);
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

        public List<Execution> GetFullExecutionsBySearchId(int? SearchId)
        {
            return _contextDbFirst.Executions
                .Include(e => e.Search)
                    .Where(e => e.SearchId == SearchId)
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

        public Search SaveSearch(Search search)
        {
            _contextDbFirst.Searches.Add(search);
            _contextDbFirst.SaveChanges();
            return search;
        }

        public Criterion SaveCriteria(Criterion criteria)
        {
            _contextDbFirst.Criteria.Add(criteria);
            _contextDbFirst.SaveChanges();
            return criteria;
        }

        public CriteriaAttribute SaveCriteriaAttribute(CriteriaAttribute criteriaAttribute)
        {
            _contextDbFirst.CriteriaAttributes.Add(criteriaAttribute);
            _contextDbFirst.SaveChanges();
            return criteriaAttribute;
        }

    }
}
