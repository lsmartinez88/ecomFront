using ecomFront.Models;
using ecomFront.Models.AnalisisViewModels;
using ecomFront.Models.DbFirstModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public interface ISharedData
    {
        List<ActivityInformation> GetActivityInformation(ApplicationUser userId);
        void CleanActivity(ApplicationUser userId);
        int GetUnreadedActivities(ApplicationUser user);
        List<ActivityInformation> GetAllActivity (ApplicationUser user);

    }
}
