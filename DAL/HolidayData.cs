using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL
{
    public class HolidayData : BaseData<Holiday>
    {
        #region Overrides
        public override List<Holiday> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Holiday> TD = (from td in db.Holidays
                                    select td).ToList();
                return TD;
            }
        }
        public override Holiday Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.Holidays
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
        public override Holiday Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            Holiday TD = new Holiday();
            TD.Id = id;
            TD.Version = version;
            db.Holidays.Attach(TD);
            db.Holidays.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, string holidayName, DateTime holidayDate, string insertUserId)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Insert(db, holidayName, holidayDate, insertUserId);
            }
        }
        public long Insert(DBDataContext db, string holidayName, DateTime holidayDate, string insertUserId)
        {
            //Create a new object 
            Holiday TD = new Holiday
            {
                HolidayName = holidayName,
                HolidayDate = holidayDate,
                InsertUserId = insertUserId,
                UpdateUserId = insertUserId,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.Holidays.InsertOnSubmit(TD);
            //send changes to the database
            db.SubmitChanges();
            return (TD.Id);
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long id, string holidayName, DateTime holidayDate, string insertUserId, string updateUserId, DateTime entryDate, Binary version)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Update(db, id, holidayName, holidayDate, insertUserId, updateUserId, entryDate, version);
            }
        }
        public bool Update(DBDataContext db, long id, string holidayName, DateTime holidayDate, string insertUserId, string updateUserId, DateTime entryDate, Binary version)
        {
            //Create a new object 
            Holiday TD = new Holiday
            {
                Id = id,
                HolidayName = holidayName,
                HolidayDate = holidayDate,
                InsertUserId = insertUserId,
                UpdateUserId = updateUserId,
                UpdateDate = DateTime.Now,
                EntryDate = entryDate,
                Version = version,
            };
            //save the record to the object model
            db.Holidays.Attach(TD, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Static Methods
        public static bool IsDateHoliday(DateTime date)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.Holidays
                          where td.HolidayDate == date.Date
                          select td);
                if (TD.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //public static int NoOfHoliday(DateTime startDate, DateTime endDate)
        //{
        //    using (SMSDataContext db = new SMSDataContext(DBHelper.GetCreditDBConnectionString()))
        //    {
        //        var TD = (from td in db.Holidays
        //                  where td.HolidayDate >= startDate
        //                  && td.HolidayDate <= endDate
        //                  select td.HolidayDate ).ToList();
        //        foreach (h in TD)
        //        {
        //        }
        //        return TD;
        //    }
        //}
        #endregion Static Methods
        #region Other Methods
        public List<Holiday> SelectSearch(string holidayname, DateTime holidaydate)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Holiday> TD = (from td in db.Holidays
                                    where holidayname != "" ? td.HolidayName.StartsWith(holidayname) : true
                                    && holidaydate != Convert.ToDateTime("01/01/1900") ? td.HolidayDate == holidaydate : true
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
        public bool IsDuplicateEntry(DBDataContext db, DateTime holidayDate)
        {
            int result = (from td in db.Holidays
                          where td.HolidayDate == holidayDate
                          select td).Count();
            return (result > 0);
        }
        #endregion Utility Methods
    }
}