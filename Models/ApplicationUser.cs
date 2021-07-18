using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace ecomFront.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Empresa { get; set; }
        [PersonalData]
        public string Nombre { get; set; }
        [PersonalData]
        public string Apellido { get; set; }
    }   
}
