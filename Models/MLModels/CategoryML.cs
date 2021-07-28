using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Models.MLModels
{
    public class CategoryML
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Picture { get; set; }
        public String Permalink { get; set; }
        public Double Total_Items_In_This_Category { get; set; }
        public List<CategoryML> Path_From_Root { get; set; }
        public List<CategoryML> Children_Categories { get; set; }
    }
}
