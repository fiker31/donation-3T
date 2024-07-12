using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL
{
    public class Donator_pData : BaseData<Donator_p>
    {
        #region Overrides
        public override List<Donator_p> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Donator_p> TD = (from td in db.Donator_ps
                                            select td).ToList();
                return TD;
            }
        }
        public override Donator_p Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.Donator_ps
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
        public override Donator_p Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            Donator_p TD = new Donator_p();
            TD.Id = id;
            TD.Version = version;
            db.Donator_ps.Attach(TD);
            db.Donator_ps.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, string FullName, string CustomerPhone, string RecursiveDonations,  string insertUserID)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Insert(db, FullName, CustomerPhone, RecursiveDonations, insertUserID);
            }

        }
        public long Insert(DBDataContext db, string FullName, string CustomerPhone, string RecursiveDonations, string insertUserID)
        {
            //Create a new object 
            Donator_p TD = new Donator_p
            {
                Fullname = FullName,
                CustomerPhone = CustomerPhone,
                RecursiveDonations = RecursiveDonations,                
                InsertUserId = insertUserID,
                UpdateUserId = insertUserID,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.Donator_ps.InsertOnSubmit(TD);
            //send changes to the database
            db.SubmitChanges();
            return (TD.Id);
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString,long id, string FullName, string CustomerPhone, string RecursiveDonations, string insertUserID, string updateUserID, DateTime entryDate, Binary version) 
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Update(db,id, FullName, CustomerPhone, RecursiveDonations, insertUserID, updateUserID, entryDate, version);
            }
        }
        public bool Update(DBDataContext db, long id,string FullName, string CustomerPhone, string RecursiveDonations, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            //Create a new object 
            Donator_p TD = new Donator_p
            {

                Fullname = FullName,
                CustomerPhone = CustomerPhone,
                RecursiveDonations = RecursiveDonations,
                InsertUserId = insertUserID,
                UpdateUserId = insertUserID,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.Donator_ps.Attach(TD, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Other Methods
        public bool IsDuplicateEntry(string Fullname)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                int result = (from td in db.Donator_ps
                              where td.Fullname == Fullname
                              select td).Count();
                return (result > 0);
            }
        }
        #endregion Other Methods
    }
}
