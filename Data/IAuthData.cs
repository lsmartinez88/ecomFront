using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public interface IAuthData
    {
        List<Auth> GetAuth();
        Boolean SaveAuth(Auth auth);
    }
}
