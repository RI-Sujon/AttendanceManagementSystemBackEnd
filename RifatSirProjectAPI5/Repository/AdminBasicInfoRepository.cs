using RifatSirProjectAPI5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Repository
{
    public class AdminBasicInfoRepository: DatabaseRepository
    {
        public AdminBasicInfo Add(AdminBasicInfo adminBasicInfo)
        {
            databaseContext.AdminBasicInfoTable.Add(adminBasicInfo);
            databaseContext.SaveChanges();
            return adminBasicInfo;
        }

        public List<AdminBasicInfo> GetAll()
        {
            return databaseContext.AdminBasicInfoTable.ToList();
        }

        public AdminBasicInfo GetByUserName(string username)
        {
            return databaseContext.AdminBasicInfoTable.SingleOrDefault(AdminBasicInfo => AdminBasicInfo.username == username);
        }

        public AdminBasicInfo Update(AdminBasicInfo adminBasicInfo)
        {
            databaseContext.AdminBasicInfoTable.Update(adminBasicInfo);
            databaseContext.SaveChanges();
            return adminBasicInfo;
        }

        public bool Delete(string username)
        {
            databaseContext.AdminBasicInfoTable.Remove(GetByUserName(username));
            databaseContext.SaveChanges();
            return true;
        }
    }
}
