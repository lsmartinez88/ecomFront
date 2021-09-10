using ecomFront.Common;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class NewSearchByCategoryModel
    {
        public NewSearchByCategoryModel()
        {

        }
        public string Nombre { set; get; }
        public string Descripcion { set; get; }
        public  int CantidadPublicaciones { set; get; }
        public List<ItemSearchCategoryResult> CategoryList { set; get; }

    }
}
