using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using CRRSDAL.Framework;

namespace CRRSDAL
{
    public class SysParmData : BaseData<SysParm>
    {
        #region Overrides

        public override List<SysParm> Select()
        {
            using (CRRSDataContext db = new CRRSDataContext(DBHelper.GetCRRSDBConnectionString()))
            {
                List<SysParm> TD = (from td in db.SysParms
                                    select td).ToList();
                return TD;
            }
        }

        public override SysParm Select(long id)
        {
            using (CRRSDataContext db = new CRRSDataContext(DBHelper.GetCRRSDBConnectionString()))
            {
                var TD = (from td in db.SysParms
                          where td.Id == id
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

        public override SysParm Select(string id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(CRRSDataContext db, long id, Binary version)
        {
            SysParm TD = new SysParm();

            TD.Id = id;
            TD.Version = version;
            db.SysParms.Attach(TD);

            db.SysParms.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }

        public override void Delete(CRRSDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }

        #endregion Overrides

        //#region Insert

        //public long Insert(string connectionString, byte minPwdLen, byte minUIdLen, byte pWDAttempts, DateTime lUDate, string workingHourFrom, string workingHourTo, string workingHourToSat, decimal totalWorkingHourPerDay, decimal totalWorkingHourPerDaySat, string lunchBreakHour, bool allowWorkOnHoliday, bool allowWorkOnWeekend, bool allowWorkAfterWorkingHours, string insertUserId)
        //{
        //    using (CRRSDataContext db = new CRRSDataContext(connectionString))
        //    {
        //        return Insert(db, minPwdLen, minUIdLen, pWDAttempts, lUDate, workingHourFrom, workingHourTo, workingHourToSat, totalWorkingHourPerDay, totalWorkingHourPerDaySat, lunchBreakHour, allowWorkOnHoliday, allowWorkOnWeekend, allowWorkAfterWorkingHours, insertUserId);
        //    }
        //}

        //public long Insert(CRRSDataContext db, byte minPwdLen, byte minUIdLen, byte pWDAttempts, DateTime lUDate, string workingHourFrom, string workingHourTo, string workingHourToSat, decimal totalWorkingHourPerDay, decimal totalWorkingHourPerDaySat, string lunchBreakHour, bool allowWorkOnHoliday, bool allowWorkOnWeekend, bool allowWorkAfterWorkingHours, string insertUserId)
        //{
        //    //Create a new object 

        //    SysParm TD = new SysParm
        //                  {
        //                      MinPwdLen = minPwdLen,
        //                      MinUIdLen = minUIdLen,
        //                      PWDAttempts = pWDAttempts,
        //                      LUDate = lUDate,
        //                      WorkingHourFrom = workingHourFrom,
        //                      WorkingHourTo = workingHourTo,
        //                      WorkingHourToSat = workingHourToSat,
        //                      TotalWorkingHourPerDay = totalWorkingHourPerDay,
        //                      TotalWorkingHourPerDaySat = totalWorkingHourPerDaySat,
        //                      LunchBreakHour = lunchBreakHour,
        //                      AllowWorkOnHoliday = allowWorkOnHoliday,
        //                      AllowWorkOnWeekend = allowWorkOnWeekend,
        //                      AllowWorkAfterWorkingHours = allowWorkAfterWorkingHours,
        //                      InsertUserId = insertUserId,
        //                      UpdateUserId = insertUserId,
        //                      UpdateDate = DateTime.Now,
        //                      EntryDate = DateTime.Now,
        //                  };
        //    //save the record to the object model
        //    db.SysParms.InsertOnSubmit(TD);
        //    //send changes to the database
        //    db.SubmitChanges();
        //    return (TD.Id);

        //}

        //#endregion Insert

        #region Update

        public bool Update(string connectionString, long id, byte minPwdLen, byte minUIdLen, byte pWDAttempts, DateTime lUDate, string workingHourFrom, string workingHourTo, string workingHourToSat, decimal totalWorkingHourPerDay, decimal totalWorkingHourPerDaySat, string lunchBreakHour, bool allowWorkOnHoliday, bool allowWorkOnWeekend, bool allowWorkAfterWorkingHours, string insertUserId, string updateUserId, DateTime entryDate, Binary version)
        {
            using (CRRSDataContext db = new CRRSDataContext(connectionString))
            {
                return Update(db, id, minPwdLen, minUIdLen, pWDAttempts, lUDate, workingHourFrom, workingHourTo, workingHourToSat, totalWorkingHourPerDay, totalWorkingHourPerDaySat, lunchBreakHour, allowWorkOnHoliday, allowWorkOnWeekend, allowWorkAfterWorkingHours, insertUserId, updateUserId, entryDate, version);
            }
        }

        public bool Update(CRRSDataContext db, long id, byte minPwdLen, byte minUIdLen, byte pWDAttempts, DateTime lUDate, string workingHourFrom, string workingHourTo, string workingHourToSat, decimal totalWorkingHourPerDay, decimal totalWorkingHourPerDaySat, string lunchBreakHour, bool allowWorkOnHoliday, bool allowWorkOnWeekend, bool allowWorkAfterWorkingHours, string insertUserId, string updateUserId, DateTime entryDate, Binary version)
        {
            //Create a new object 

            SysParm TD = new SysParm
                          {
                              Id = id,
                              MinPwdLen = minPwdLen,
                              MinUIdLen = minUIdLen,
                              PWDAttempts = pWDAttempts,
                              LUDate = lUDate,
                              WorkingHourFrom = workingHourFrom,
                              WorkingHourTo = workingHourTo,
                              WorkingHourToSat = workingHourToSat,
                              TotalWorkingHourPerDay = totalWorkingHourPerDay,
                              TotalWorkingHourPerDaySat = totalWorkingHourPerDaySat,
                              LunchBreakHour = lunchBreakHour,
                              AllowWorkOnHoliday = allowWorkOnHoliday,
                              AllowWorkOnWeekend = allowWorkOnWeekend,
                              AllowWorkAfterWorkingHours = allowWorkAfterWorkingHours,
                              InsertUserId = insertUserId,
                              UpdateUserId = updateUserId,
                              UpdateDate = DateTime.Now,
                              EntryDate = entryDate,
                              Version = version,
                          };
            //save the record to the object model
            db.SysParms.Attach(TD, true);
            //send changes to the database
            db.SubmitChanges();
            return true;

        }

        #endregion Update

        #region Other Methods

        public List<SysParm> SelectSearch()
        {
            using (CRRSDataContext db = new CRRSDataContext(DBHelper.GetCRRSDBConnectionString()))
            {
                List<SysParm> TD = (from td in db.SysParms
                                    select td).ToList();
                return TD;
            }
        }

        #endregion Other Methods

        #region Utility Methods

        /// <summary>
        /// Checks to see that Account No is unique for selected period and branch.
        /// </summary>
        /// <returns>Returns true if the Entry is already in database for the period for the branch</returns>
        public bool IsDuplicateEntry(CRRSDataContext db, long id)
        {

            int result = (from td in db.SysParms
                          where td.Id != id
                          select td).Count();

            return (result > 0);
        }

        #endregion Utility Methods

    }
}