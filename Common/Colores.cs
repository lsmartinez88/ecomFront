using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Common
{
    public static class Colores
    {
        static string[] _colores = new string [21];
        static string[] _styles = new string[8];

        static Colores()
        {
             _colores[0] = "#25b372";
             _colores[1] = "#45748a";
             _colores[2] = "#f58646";
             _colores[3] = "#2196F3";
             _colores[4] = "#8e70c1";
             _colores[5] = "#ffd648";
             _colores[6] = "#f35c86";
             _colores[7] =  "#333"  ;
             _colores[8] = "#3F51B5";
             _colores[9] = "#2196F3";
             _colores[10] = "#00BCD4";
             _colores[11] = "#e9f5fe";
             _colores[12] = "#45748a";
             _colores[13] = "#e9f5fe";
             _colores[14] = "#45748a";
             _colores[15] = "#fdeeee";
             _colores[16] = "#fde7da";
             _colores[17] = "#25b372";
             _colores[18] = "#d3f0e3";
             _colores[19] = "#00BCD4";
             _colores[20] = "#ccf2f6";

            _styles[0] = "primary";
            _styles[1] = "success";
            _styles[2] = "warning";
            _styles[3] = "info";
            _styles[4] = "teal";
            _styles[5] = "danger";
            _styles[6] = "dark";
            _styles[7] = "secondary";

        }

        public static string GetColor(int index)
        {
            return _colores[index];
        }

        public static string GetStyle(int index)
        {
            return _styles[index];
        }

    }
}
