using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL
{
    public class DonationReg_ListData : BaseData<DonationReg_List>
    {
        #region Overrides
        public override List<DonationReg_List> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<DonationReg_List> TD = (from td in db.DonationReg_Lists
                                            select td).ToList();
                return TD;
            }
        }
        public override DonationReg_List Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.DonationReg_Lists
                          where td.id == id
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
        public override DonationReg_List Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            DonationReg_List TD = new DonationReg_List();
            TD.id = id;
            TD.Version = version;
            db.DonationReg_Lists.Attach(TD);
            db.DonationReg_Lists.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, int companyID, string DonationTitle, string DonationDescription, int DonationAmountRequired,Binary poster,  DateTime Donationenddate, int specialshortcode, string relatedlinks, string sms, string email, string insertUserID)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Insert(db, companyID, DonationTitle, DonationDescription, DonationAmountRequired, poster,  Donationenddate, specialshortcode, relatedlinks, sms,email, insertUserID);
            }

        }
        public long Insert(DBDataContext db, int companyID, string DonationTitle, string DonationDescription, int DonationAmountRequired, Binary poster,  DateTime Donationenddate, int specialshortcode, string relatedlinks, string sms, string email, string insertUserID)
        {
            //Create a new object 
            DonationReg_List TD = new DonationReg_List
            {
                companyid = companyID,
                donationtitle = DonationTitle,
                donationdescription = DonationDescription,
                donationamountrequired = DonationAmountRequired,
				poster = poster,
                donationenddate = Donationenddate,
                specialshortcode = specialshortcode,
                donationrelatedlinks = relatedlinks,
                sms=sms,
                email=email,
                InsertUserId = insertUserID,
                UpdateUserId = insertUserID,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.DonationReg_Lists.InsertOnSubmit(TD);
            //send changes to the database
            db.SubmitChanges();
            return (TD.id);
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionstring , long id, int companyid, string connectionString, int companyID, string DonationTitle, string DonationDescription, int DonationAmountRequired,Binary poster,  DateTime Donationenddate, int specialshortcode, string relatedlinks, string sms, string email, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            using (DBDataContext db = new DBDataContext(connectionstring))
            {
                return Update (db,id, companyID, DonationTitle, DonationDescription, DonationAmountRequired, poster,  Donationenddate, specialshortcode, relatedlinks, sms, email, insertUserID, updateUserID, entryDate, version);
            }
        }
        public bool Update(DBDataContext db, long id, int companyID, string DonationTitle, string DonationDescription, int DonationAmountRequired,Binary poster, DateTime Donationenddate, int specialshortcode, string relatedlinks, string sms, string email, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            //Create a new object 
            DonationReg_List TD = new DonationReg_List
            {

                companyid = companyID,
                donationtitle = DonationTitle,
                donationdescription = DonationDescription,
                donationamountrequired = DonationAmountRequired,
                poster=poster,
                donationenddate = Donationenddate,
                specialshortcode = specialshortcode,
                donationrelatedlinks = relatedlinks,
                sms = sms,
                email = email,
                InsertUserId = insertUserID,
                UpdateUserId = updateUserID,
                UpdateDate = DateTime.Now,
                EntryDate = entryDate,
                Version = version,
            };
            //save the record to the object model
            db.DonationReg_Lists.Attach(TD, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Other Methods
        public bool IsDuplicateEntry(string DonationTitle)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                int result = (from td in db.DonationReg_Lists
                              where td.donationtitle == DonationTitle
                              select td).Count();
                return (result > 0);
            }
        }
        #endregion Other Methods
    }
}
