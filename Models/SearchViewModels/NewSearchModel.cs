using ecomFront.Common;
using ecomFront.Models.DbFirstModels;
using ecomFront.Models.MLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.SearchViewModels
{
    public class NewSearchModel
    {
        public NewSearchModel()
        {
            atributosSeleccionados = new List<MLModels.Attribute>();
        }
        public string Nombre { set; get; }
        public string Descripcion { set; get; }
        public  int CantidadPublicaciones { set; get; }
        public ItemSearchResultInformation itemEncontrado { set; get; }
        public List<MLModels.Attribute> atributosSeleccionados { set; get; }

    }
}
