using ecomFront.Common;
using ecomFront.Data;
using ecomFront.Models;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.SearchViewModels;
using ecomFront.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Mime;

namespace ecomFront.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SearchController> _logger;
        private ISearchData _searchData;

        public SearchController(UserManager<ApplicationUser> userManager,
                                ILogger<SearchController> logger,
                                ISearchData searchData)
        {
            _userManager = userManager;
            _logger = logger;
            _searchData = searchData;
        }

        public IActionResult Searches()
        {
            var userid = _userManager.GetUserId(HttpContext.User);

            var searchesVM = new SearchesViewModel
            {
                Searches = _searchData.GetFullSearches(userid)
            };

            foreach(var search in searchesVM.Searches.Where(s => !string.IsNullOrEmpty(s.ListingPermalink)))
            {
                var searchItem = new SearchRelatedItem();
                searchItem.item = MLService.GetItemByPermaLink(search.ListingPermalink);
                searchItem.searchId = search.IdSearch;
                searchesVM.RelatedItems.Add(searchItem);
            }

            return View(searchesVM);
        }

        public IActionResult Criteria(int? searchId)
        {
            var criteriaListViewModel = new CriteriaListViewModel
            {
                search = _searchData.GetSearch(searchId)
            };

            var criterias = _searchData.GetFullCriteriasBySearchId(searchId);
            foreach (var criteria in criterias)
            {
                var cti = new CriteriaItemList(criteria);
                criteriaListViewModel.criteriaItems.Add(cti);
            }
            return View(criteriaListViewModel);
        }

        public IActionResult Executions(int? searchId)
        {
            var executionsListViewModel = new ExecutionsListViewModel
            {
                search = _searchData.GetSearch(searchId),
                ExecutionsList = _searchData.GetFullExecutionsBySearchId(searchId)
            };
            return View(executionsListViewModel);
        }

        public async System.Threading.Tasks.Task<IActionResult> HomeAddSearch()
        {
            var newSearchVM = new NewSearchViewModel
            {
                user = await _userManager.GetUserAsync(HttpContext.User)
            };
            return View(newSearchVM);
        }

        public async System.Threading.Tasks.Task<IActionResult> SearchByCategory()
        {
            var searchByCategory = new SearchByCategoryViewModel
            {
                user = await _userManager.GetUserAsync(HttpContext.User)
            };
            return View(searchByCategory);
        }

        public async System.Threading.Tasks.Task<IActionResult> SearchByLink()
        {
            var searchByLink = new SearchByLinkViewModel
            {
                user = await _userManager.GetUserAsync(HttpContext.User)
            };
            return View(searchByLink);
        }

        public async System.Threading.Tasks.Task<IActionResult> SearchByTitle()
        {
            var searchByTitle = new SearchByTitleViewModel
            {
                user = await _userManager.GetUserAsync(HttpContext.User)
            };
            return View(searchByTitle);
        }


        [HttpPost]
        public JsonResult BuscarPorLink(string link)
        {
            try
            {
                Models.MLModels.Item item = MLService.GetItemByPermaLink(link);
                if (item == null)
                {
                    throw new Exception("No se encontro el link buscado. Intenta Nuevamente!");
                }

                var itemLinkViewModel = new ItemSearchResultInformation
                {
                    id = item.id,
                    title = item.title,
                    subtitle = (string)item.subtitle,
                    category_id = item.category_id,
                    category = MLService.GetCategoryById(item.category_id),
                    price = item.price,
                    sold_quantity = item.sold_quantity,
                    date_created = item.date_created.ToString(format: "dd MMM yyyy"),
                    condition = item.condition,
                    permalink = item.permalink,
                    thumbnail = item.thumbnail,
                    pictures = item.pictures.ToList(),
                    descriptions = item.descriptions.ToList(),
                    attributes = item.attributes.ToList()
                };

                return Json(itemLinkViewModel);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.NoContent;
                return Json(new { responseText = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult SaveNewSearchByLink([FromBody] NewSearchModel model)
        {
            Models.DbFirstModels.Search newSearch;
            Criterion newCriteria;
            try
            {
                var search = new Models.DbFirstModels.Search
                {
                    Description = model.Nombre,
                    Name = model.Nombre,
                    ListingPermalink = model.itemEncontrado.permalink,
                    SearchType = SearchType.Publicacion,
                    Version = 0,
                    UserId = _userManager.GetUserId(HttpContext.User)
                };

                newSearch = _searchData.SaveSearch(search);
            }
            catch(Exception ex)
            {
                return Json(new { result = "ERROR", errorMessage = "Error Guardando el Search: " + ex.Message });
            }

            try
            {
                var criteria = new Criterion
                {
                    CategoryId = model.itemEncontrado.category_id,
                    ItemCondition = model.itemEncontrado.condition,
                    Quantity = (long)model.CantidadPublicaciones,
                    SearchId = newSearch.IdSearch,
                    SearchCriteria = "",
                    Version = 0
                };

                newCriteria = _searchData.SaveCriteria(criteria);
            }          
            catch(Exception ex)
            {
                return Json(new { result = "ERROR", errorMessage = "Error Guardando el Criterio: " + ex.Message });
            }

            try
            {
                foreach(var attribute in model.atributosSeleccionados)
                {
                    if(!string.IsNullOrEmpty(attribute.name) && !string.IsNullOrEmpty(attribute.value_id) && !string.IsNullOrEmpty(attribute.id) && !string.IsNullOrEmpty(attribute.value_name))
                    {
                        var criteria_attribute = new CriteriaAttribute
                        {
                            CriteriaId = (long)newCriteria.IdCriteria,
                            IdAttributeml = attribute.id,
                            IdAttributeValueml = attribute.value_id,
                            NameAttributeml = attribute.name,
                            NameAttributeValueml = attribute.value_name,
                            Version = 0
                        };
                        _searchData.SaveCriteriaAttribute(criteria_attribute);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", errorMessage = "Error Guardando el Criterio: " + ex.Message });
            }

            return Json(new { result = "OK", successMessage = "El id " + newSearch.IdSearch + " fue creado correctamente!!" });
        }
              

        [HttpPost]
        public JsonResult SaveNewSearchByCategory([FromBody] NewSearchByCategoryModel model)
        {
            Models.DbFirstModels.Search newSearch;
            Criterion newCriteria;
            try
            {
                var search = new Models.DbFirstModels.Search
                {
                    Description = model.Nombre,
                    Name = model.Nombre,
                    SearchType = SearchType.Categoria,
                    Version = 0,
                    UserId = _userManager.GetUserId(HttpContext.User)
                };

                newSearch = _searchData.SaveSearch(search);
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", errorMessage = "Error Guardando el Search: " + ex.Message });
            }

            try
            {
                foreach (var category in model.CategoryList)
                {
                    var criteria = new Criterion
                    {
                        CategoryId = category.categoryId,
                        ItemCondition = ItemCondition.Nuevo,
                        Quantity = (long)category.attributes.Sum(a => Int32.Parse(a.quantity)),
                        SearchId = newSearch.IdSearch,
                        SearchCriteria = "",
                        Version = 0
                    };

                    newCriteria = _searchData.SaveCriteria(criteria);


                    foreach (var attribute in category.attributes)
                    {
                        if (!string.IsNullOrEmpty(attribute.name) && !string.IsNullOrEmpty(attribute.value_id) && !string.IsNullOrEmpty(attribute.value) && !string.IsNullOrEmpty(attribute.value_name))
                        {
                            var criteria_attribute = new CriteriaAttribute
                            {
                                CriteriaId = (long)newCriteria.IdCriteria,
                                IdAttributeml = attribute.value_id,
                                IdAttributeValueml = attribute.value,
                                NameAttributeml = attribute.name,
                                NameAttributeValueml = attribute.value_name,
                                Version = 0
                            };
                            _searchData.SaveCriteriaAttribute(criteria_attribute);
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", errorMessage = "Error Guardando el Criterio: " + ex.Message });
            }

            return Json(new { result = "OK", successMessage = "El id " + newSearch.IdSearch + " fue creado correctamente!!" });
        }

        [HttpPost]
        public JsonResult SaveNewSearchByTitle([FromBody] NewSearchByTitleModel model)
        {
            Models.DbFirstModels.Search newSearch;
            Criterion newCriteria;
            try
            {
                var search = new Models.DbFirstModels.Search
                {
                    Description = model.Nombre,
                    Name = model.Nombre,
                    SearchType = SearchType.Categoria,
                    Version = 0,
                    UserId = _userManager.GetUserId(HttpContext.User)
                };

                newSearch = _searchData.SaveSearch(search);
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", errorMessage = "Error Guardando el Search: " + ex.Message });
            }

            try
            {
                foreach (var category in model.CategoryList)
                {
                    var criteria = new Criterion
                    {
                        CategoryId = category.categoryId,
                        ItemCondition = ItemCondition.Nuevo,
                        Quantity = (long)Int32.Parse(category.categoryQtty),
                        SearchId = newSearch.IdSearch,
                        SearchCriteria = model.SearchCriteria,
                        Version = 0
                    };

                    newCriteria = _searchData.SaveCriteria(criteria);
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "ERROR", errorMessage = "Error Guardando el Criterio: " + ex.Message });
            }

            return Json(new { result = "OK", successMessage = "El id " + newSearch.IdSearch + " fue creado correctamente!!" });
        }

        [HttpPost]
        public JsonResult DeleteSearchById(int idSearch)
        {
            try
            {
                if(_searchData.DeleteSearchById(idSearch))
                {
                    return Json(new { result = "OK" });
                }
                else
                {
                    throw new Exception();
                }   
            }
            catch
            {
                return Json(new { result = "ERROR", errorMessage = "No se pudo borrar la busqueda"});
            }
        }


        [HttpPost]
        public JsonResult CloneSearch(int idSearch)
        {
            try
            {
                var newSearch = _searchData.CloneSearch(idSearch);
                if (newSearch != null)
                {
                    return Json(new { result = "OK", successMessage = "La búsqueda se ha clonado con éxito!" });
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                return Json(new { result = "ERROR", errorMessage = "No se pudo clonar la búsqueda" });
            }
        }

    }
}
