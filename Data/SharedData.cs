using ecomFront.Common;
using ecomFront.Models;
using ecomFront.Models.AnalisisViewModels;
using ecomFront.Models.DbFirstModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public class SharedData : ISharedData
    {
        private ApplicationDbContext _contextModel;
        private DBFirstDbContext _contextDbFirst;

        public SharedData(DBFirstDbContext contextDbFirst,
                          ApplicationDbContext contextModel)
        {
            _contextDbFirst = contextDbFirst;
            _contextModel = contextModel;
        }

        public void CleanActivity(ApplicationUser userId)
        {
            var lista = _contextModel.ActivityInformation.Where(ac => ac.user == userId && ac.Estado == 0).ToList();
            lista.ForEach(a => a.Estado = 1);
            _contextModel.SaveChanges();
        }

        public List<ActivityInformation> GetActivityInformation(ApplicationUser userId)
        {
            return _contextModel.ActivityInformation.Where(ac => ac.user == userId && ac.Estado == 0).OrderByDescending(ac => ac.Fecha).ToList();
        }
    }
}
