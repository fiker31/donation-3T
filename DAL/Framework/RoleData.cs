using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
namespace DAL.Framework
{
    public class RoleData : BaseData<EntRole>
    {
        #region Overrides
        public override List<EntRole> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<EntRole> R = (from r in db.EntRoles
                                   select r).ToList();
                return R;
            }
        }
        public override EntRole Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var R = (from r in db.EntRoles
                         where r.RoleId == id
                         select r);
                if (R.Count() != 0)
                {
                    return R.Single();
                }
                else
                {
                    return null;
                }
            }
        }
        public override EntRole Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            //Create Role object
            EntRole R = new EntRole();
            R.RoleId = id;
            R.Version = version;
            db.EntRoles.Attach(R);
            db.EntRoles.DeleteOnSubmit(R);
            db.SubmitChanges();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, string roleName, string userId)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Insert(db, roleName, userId);
            }
        }
        public long Insert(DBDataContext db, string roleName, string userId)
        {
            //Create a new Role object
            EntRole R = new EntRole
            {
                RoleName = roleName,
                InsertUserId = userId,
                EntryDate = DateTime.Now,
                UpdateUserId = userId,
                UpdateDate = DateTime.Now
            };
            //save the record to the object model
            db.EntRoles.InsertOnSubmit(R);
            //send changes to the database
            db.SubmitChanges();
            return R.RoleId;
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long id, string roleName,
            string insertUserId, DateTime entryDate, string userId, Binary version)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Update(db, id, roleName
                    , insertUserId, entryDate, userId, version);
            }
        }
        public bool Update(DBDataContext db, long id, string roleName,
            string insertUserId, DateTime entryDate, string userId, Binary version)
        {
            //Create a new Role object
            EntRole R = new EntRole
            {
                RoleId = id,
                RoleName = roleName,
                InsertUserId = insertUserId,
                EntryDate = entryDate,
                UpdateUserId = userId,
                UpdateDate = DateTime.Now,
                Version = version
            };
            //save the record to the object model
            db.EntRoles.Attach(R, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Utility Methods
        public bool IsDuplicateRoleName(DBDataContext db, long roleId, string roleName)
        {
            return IsDuplicate(db, "EntRole", "RoleName", "RoleId", roleName, roleId);
        }
        public List<EntRole> SelectByUserAccountId(long userAccountId)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<EntRole> R = (from r in db.EntRoles
                                   join ru in db.EntRoleUserAccounts
                                   on r.RoleId equals ru.RoleId
                                   where ru.UserAccountId == userAccountId
                                   select r).ToList();
                return R;
            }
        }
        public List<EntRole> SelectByRoleName(string roleName)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<EntRole> R = (from r in db.EntRoles
                                   where r.RoleName.StartsWith(roleName)
                                   select r).ToList();
                return R;
            }
        }
        #endregion Utility Methods
    }
}
