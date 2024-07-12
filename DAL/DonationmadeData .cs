using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL
{
    public class DonationmadeData : BaseData<Donationmade>
    {
        #region Overrides
        public override List<Donationmade> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Donationmade> TD = (from td in db.Donationmades
                                            select td).ToList();
                return TD;
            }
        }
        public override Donationmade Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.Donationmades
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
        public override Donationmade Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            Donationmade TD = new Donationmade();
            TD.Id = id;
            TD.Version = version;
            db.Donationmades.Attach(TD);
            db.Donationmades.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, int DonationID, string CustomerPhone, string CustomerName, string DonationAmount,  DateTime DonationDate,  string insertUserID)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Insert(db, DonationID, CustomerPhone, CustomerName, DonationAmount, DonationDate, insertUserID);
            }

        }

        public long Insert(DBDataContext db, int DonationID, string CustomerPhone, string CustomerName, string DonationAmount, DateTime DonationDate, string insertUserID)
        {
            //Create a new object 
            Donationmade TD = new Donationmade
            {
                DonationId = DonationID,
                CustomerPhone = CustomerPhone,
                CustomerName = CustomerName,
                DonationAmount = DonationAmount,
                DonationDate = DonationDate,
                InsertUserId = insertUserID,
                UpdateUserId = insertUserID,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.Donationmades.InsertOnSubmit(TD);
            //send changes to the database
            db.SubmitChanges();
            return (TD.Id);
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long id, int DonationID, string CustomerPhone, string CustomerName, string DonationAmount, DateTime DonationDate, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Update(db,id, DonationID, CustomerPhone, CustomerName, DonationAmount, DonationDate, insertUserID, updateUserID, entryDate, version);
            }
        }
        public bool Update (DBDataContext db, long id, int DonationID, string CustomerPhone, string CustomerName, string DonationAmount, DateTime DonationDate, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            //Create a new object 
            Donationmade TD = new Donationmade
            {
                Id=id,
                DonationId = DonationID,
                CustomerPhone = CustomerPhone,
                CustomerName = CustomerName,
                DonationAmount = DonationAmount,
                DonationDate = DonationDate,
                InsertUserId = insertUserID,
                UpdateUserId = updateUserID,
                UpdateDate = DateTime.Now,
                EntryDate = entryDate,
                Version = version,
            };
            //save the record to the object model
            db.Donationmades.Attach(TD, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Other Methods
        public bool IsDuplicateEntry(string Customername)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                int result = (from td in db.Donationmades
                              where td.CustomerName == Customername
                              select td).Count();
                return (result > 0);
            }
        }
        #endregion Other Methods
    }
}
