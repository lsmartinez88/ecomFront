
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

        public List<Models.DbFirstModels.Search> GetFullSearches(string UserId)
        {
            return _contextDbFirst.Searches
                        .Include(s => s.Criteria)
                        .ThenInclude(cri => cri.Category)
                        .Include(s => s.Criteria)
                        .ThenInclude(c => c.CriteriaAttributes)
                        .Include(s => s.Executions)
                        .Where(s => s.UserId == UserId && s.Status == 1)
                        .AsSplitQuery()
                        .ToList();
        }


        public Models.DbFirstModels.Search GetSearch(int? searchId)
        {
            return _contextDbFirst.Searches
                .FirstOrDefault(s => s.IdSearch == searchId);
        }

        public Models.DbFirstModels.Search GetSearchWithExecutions(int? searchId)
        {
            return _contextDbFirst.Searches.Include(s =>s.Executions)
                .FirstOrDefault(s => s.IdSearch == searchId);
        }
        public Models.DbFirstModels.Search SaveSearch(Models.DbFirstModels.Search search)
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

        public List<SalesQuantityMap> GetSalesMap(int ExecutionId, SalesType tipo)
        {
           return _contextModel.SalesPerCity.Where(a => a.ExecutionId == ExecutionId).Where(a => (tipo == SalesType.Compra ?  a.CantidadCompras != 0 : a.CantidadVentas != 0)).GroupBy(x => new { x.ExecutionId, x.City, x.State, x.Latitud, x.Longitud })
                .Select(x => new SalesQuantityMap
                {
                    ExecutionId = x.Key.ExecutionId,
                    City = x.Key.City.Replace("_",""),
                    Latitud = x.Key.Latitud,
                    Longitud = x.Key.Longitud,
                    Tipo = tipo,
                    Cantidad = x.Sum(i => (tipo == SalesType.Compra ? i.CantidadCompras : i.CantidadVentas))

                }).ToList();
        }

        public List<SalesQuantityMap> GetSalesMapCriteria(int ExecutionId, int CriteriaId, SalesType tipo)
        {
            return _contextModel.SalesPerCity.Where(a => a.ExecutionId == ExecutionId).Where(a => (tipo == SalesType.Compra ? a.CantidadCompras != 0 : a.CantidadVentas != 0))
                 .Select(x => new SalesQuantityMap
                 {
                     ExecutionId = x.ExecutionId,
                     CriteriaId = x.CriteriaId,
                     City = x.City.Replace("_", ""),
                     Latitud = x.Latitud,
                     Longitud = x.Longitud,
                     Tipo = tipo,
                     Cantidad = (tipo == SalesType.Compra ? x.CantidadCompras : x.CantidadVentas)

                 }).ToList();
        }

        public List<MainDashboard> GetMainDashboards(String userId)
        {
            return _contextModel.MainDashboard.Where(a => a.UserId.Equals(userId)).ToList();
        }

        public List<MainDashboard> GetSellersTypeSearch(int searchId)
        {
            return _contextModel.MainDashboard.Where(a => a.SearchId.Equals(searchId) && a.ParameterName.Contains("CantidadCompetidores_")).ToList();
        }

        public List<MainDashboard> GetAvgPriceSearch(int searchId)
        {
            return _contextModel.MainDashboard.Where(a => a.SearchId.Equals(searchId) && a.ParameterName.Contains("EjecucionPrecioMedio_")).ToList();
        }

        public List<MainDashboard> GetAvgSellPriceSearch(int searchId)
        {
            return _contextModel.MainDashboard.Where(a => a.SearchId.Equals(searchId) && a.ParameterName.Contains("EjecucionPrecioMedioVenta_")).ToList();
        }


        public bool DeleteSearchById(int searchId)
        {
            try
            {
                var search = _contextDbFirst.Searches.FirstOrDefault(search => search.IdSearch == searchId);
                search.Status = 0;
                _contextDbFirst.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public Models.DbFirstModels.Search CloneSearch(int searchId)
        {
            var search = _contextDbFirst.Searches
                        .Include(s => s.Criteria)
                        .ThenInclude(cri => cri.Category)
                        .Include(s => s.Criteria)
                        .ThenInclude(c => c.CriteriaAttributes)
                        .Include(s => s.Executions)
                        .FirstOrDefault(s => s.IdSearch == searchId);

            var newSearch = new Models.DbFirstModels.Search
            {
                CatalogProductIdml = search.CatalogProductIdml,
                Description = search.Description,
                ListingPermalink = search.ListingPermalink,
                Name = "Copia de " + search.Name,
                SearchType = search.SearchType,
                Status = 1,
                UserId = search.UserId,
                Version = search.Version
            };

            _contextDbFirst.Searches.Add(newSearch);
            _contextDbFirst.SaveChanges();

            foreach (var citerio in search.Criteria)
            {
                var nuevoCriterio = new Criterion
                {
                    Category = citerio.Category,
                    CategoryId = citerio.CategoryId,
                    ItemCondition = citerio.ItemCondition,
                    RelatedLink = citerio.RelatedLink,
                    SearchCriteria = citerio.SearchCriteria,
                    Version = citerio.Version,
                    SearchId = newSearch.IdSearch
                };

                _contextDbFirst.Criteria.Add(nuevoCriterio);
                _contextDbFirst.SaveChanges();

                foreach (var criteriaAttribute in citerio.CriteriaAttributes)
                {
                    var newCriterioAttribute = new CriteriaAttribute
                    {
                        IdAttributeml = criteriaAttribute.IdAttributeml,
                        IdAttributeValueml = criteriaAttribute.IdAttributeValueml,
                        NameAttributeml = criteriaAttribute.NameAttributeml,
                        NameAttributeValueml = criteriaAttribute.NameAttributeValueml,
                        Version = criteriaAttribute.Version,
                        CriteriaId = nuevoCriterio.IdCriteria
                    };
                    _contextDbFirst.CriteriaAttributes.Add(newCriterioAttribute);
                    _contextDbFirst.SaveChanges();
                }

            }

            return newSearch;
        }
    }
}
