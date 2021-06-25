using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RifatSirProjectAPI5.Models;
using RifatSirProjectAPI5.Repository;

namespace RifatSirProjectAPI5.Repository
{
    public class AdminAuthRepository: DatabaseRepository
    {
        public AdminAuth Add(AdminAuth adminAuth)
        {
            databaseContext.AdminAuthTable.Add(adminAuth);
            databaseContext.SaveChanges();
            return adminAuth;
        }

        /*public List<AdminAuth> GetAll()
        {
            return databaseContext.AdminAuthTable.ToList();
        }*/

        public AdminAuth GetByUserName(string username)
        {
            return databaseContext.AdminAuthTable.SingleOrDefault(AdminAuth => AdminAuth.username == username);
        }

        public AdminAuth Update(AdminAuth adminAuth)
        {
            databaseContext.AdminAuthTable.Update(adminAuth);
            databaseContext.SaveChanges();
            return adminAuth;
        }

        public bool Delete(string username)
        {
            databaseContext.AdminAuthTable.Remove(GetByUserName(username));
            databaseContext.SaveChanges();
            return true;
        }
    }
}
