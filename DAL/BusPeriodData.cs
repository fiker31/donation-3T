using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL
{
    public class BusPeriodData : BaseData<BusPeriod>
    {
        #region Overrides
        public override List<BusPeriod> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<BusPeriod> TD = (from td in db.BusPeriods
                                      select td).ToList();
                return TD;
            }
        }
        public override BusPeriod Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.BusPeriods
                          where td.RowId == id
                          select td);
                if (TD.Count() != 0)
                {
                    return TD.Single();
                }
                else
                {
                    return null;
                }
            }
        }
        public BusPeriod SelectCurrentPeriod()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.BusPeriods
                          where td.PStatus == 0
                          select td);
                if (TD.Count() != 0)
                {
                    return TD.Single();
                }
                else
                {
                    return null;
                }
            }
        }
        public override BusPeriod Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            BusPeriod TD = new BusPeriod();
            TD.RowId = id;
            TD.Version = version;
            db.BusPeriods.Attach(TD);
            db.BusPeriods.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, string businessPeriod, byte pStatus, string insertUserId)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Insert(db, businessPeriod, pStatus, insertUserId);
            }
        }
        public long Insert(DBDataContext db, string businessPeriod, byte pStatus, string insertUserId)
        {
            //Create a new object 
            BusPeriod TD = new BusPeriod
            {
                BusinessPeriod = businessPeriod,
                PStatus = pStatus,
                InsertUserId = insertUserId,
                UpdateUserId = insertUserId,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.BusPeriods.InsertOnSubmit(TD);
            //send changes to the database
            db.SubmitChanges();
            return (TD.RowId);
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long rowId, string businessPeriod, byte pStatus, string insertUserId, string updateUserId, DateTime entryDate, Binary version)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Update(db, rowId, businessPeriod, pStatus, insertUserId, updateUserId, entryDate, version);
            }
        }
        public bool Update(DBDataContext db, long rowId, string businessPeriod, byte pStatus, string insertUserId, string updateUserId, DateTime entryDate, Binary version)
        {
            //Create a new object 
            BusPeriod TD = new BusPeriod
            {
                RowId = rowId,
                BusinessPeriod = businessPeriod,
                PStatus = pStatus,
                InsertUserId = insertUserId,
                UpdateUserId = updateUserId,
                UpdateDate = DateTime.Now,
                EntryDate = entryDate,
                Version = version,
            };
            //save the record to the object model
            db.BusPeriods.Attach(TD, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Other Methods
        #endregion Other Methods
        #region Utility Methods
        #endregion Utility Methods
    }
}