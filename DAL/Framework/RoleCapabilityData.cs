using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL.Framework
{
    public class RoleCapabilityData : BaseData<EntRoleCapability>
    {
        #region Overrides
        public override List<EntRoleCapability> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<EntRoleCapability> RC = (from rc in db.EntRoleCapabilities
                                              select rc).ToList();
                return RC;
            }
        }
        public override EntRoleCapability Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var RC = (from rc in db.EntRoleCapabilities
                          where rc.RoleCapabilityId == id
                          select rc);
                if (RC.Count() != 0)
                {
                    return RC.Single();
                }
                else
                {
                    return null;
                }
            }
        }
        public override EntRoleCapability Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            //Create Role Capability object
            EntRoleCapability RC = new EntRoleCapability();
            RC.RoleCapabilityId = id;
            RC.Version = version;
            db.EntRoleCapabilities.Attach(RC);
            db.EntRoleCapabilities.DeleteOnSubmit(RC);
            db.SubmitChanges();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, long roleId, long capabilityId,
            byte accessFlag, string userId)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Insert(db, roleId, capabilityId, accessFlag, userId);
            }
        }
        public long Insert(DBDataContext db, long roleId, long capabilityId,
            byte accessFlag, string userId)
        {
            //Create a new Role Capability object
            EntRoleCapability R = new EntRoleCapability
            {
                RoleId = roleId,
                CapabilityId = capabilityId,
                AccessFlag = accessFlag,
                InsertUserId = userId,
                EntryDate = DateTime.Now,
                UpdateUserId = userId,
                UpdateDate = DateTime.Now
            };
            //save the record to the object model
            db.EntRoleCapabilities.InsertOnSubmit(R);
            //send changes to the database
            db.SubmitChanges();
            return R.RoleCapabilityId;
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long id, long roleId, long capabilityId, byte accessFlag,
            string insertUserId, DateTime entryDate, string userId, Binary version)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Update(db, id, roleId, capabilityId, accessFlag
                    , insertUserId, entryDate, userId, version);
            }
        }
        public bool Update(DBDataContext db, long id, long roleId, long capabilityId, byte accessFlag,
            string insertUserId, DateTime entryDate, string userId, Binary version)
        {
            //Create a new Role Capability object
            EntRoleCapability R = new EntRoleCapability
            {
                RoleCapabilityId = id,
                RoleId = roleId,
                CapabilityId = capabilityId,
                AccessFlag = accessFlag,
                InsertUserId = insertUserId,
                EntryDate = entryDate,
                UpdateUserId = userId,
                UpdateDate = DateTime.Now,
                Version = version
            };
            //save the record to the object model
            db.EntRoleCapabilities.Attach(R, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Custom Select
        public List<EntRoleCapability> SelectByRoleId(long roleId)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<EntRoleCapability> RC = (from rc in db.EntRoleCapabilities
                                              where rc.RoleId == roleId
                                              select rc).ToList();
                return RC;
            }
        }
        #endregion Custom Select
    }
}
