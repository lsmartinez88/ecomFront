using ecomFront.Models.DbFirstModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomFront.Data
{
    public class AuthData : IAuthData
    {
        private ApplicationDbContext _contextModel;
        private DBFirstDbContext _contextDbFirst;

        public AuthData(DBFirstDbContext contextDbFirst,
                          ApplicationDbContext contextModel)
        {
            _contextDbFirst = contextDbFirst;
            _contextModel = contextModel;
        }

        public List<Auth> GetAuth()
        {
            return _contextDbFirst.Auths.ToList();
        }

        public bool SaveAuth(Auth auth)
        {
            int resultado = 0;
            try { 
                if(auth.Id == 0)
                {
                    _contextDbFirst.Auths.Add(auth);
                    resultado = _contextDbFirst.SaveChanges();
                } else
                {
                    Auth authDB = _contextDbFirst.Auths.Find(auth.Id);
                    _contextDbFirst.Entry(authDB).CurrentValues.SetValues(auth);
                    _contextDbFirst.Entry(authDB).State = EntityState.Modified;
                    resultado = _contextDbFirst.SaveChanges();
                }

                return !(resultado == 0);
            } catch (Exception e) {
                return false;
            }


        }
    }
}
