using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL
{
    public class Donationlist_RegData : BaseData<DonationReg_Reg>
    {
        #region Overrides
        public override List<Donationlist_Reg> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Donationlist_Reg> TD = (from td in db.Donationlist_Regs
                                            select td).ToList();
                return TD;
            }
        }
        public override Donationlist_Reg Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.Donationlist_Regs
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
        public override Donationlist_Reg Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            Donationlist_Reg TD = new Donationlist_Reg();
            TD.id = id;
            TD.Version = version;
            db.Donationlist_Regs.Attach(TD);
            db.Donationlist_Regs.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, string companyname, string companydescription, string location, int phone, string pobox, string generalmanager, int tillnumber, DateTime formationDate, string insertUserID)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Insert(db,  companyname,  companydescription,  location,  phone,  pobox,  generalmanager,  tillnumber,formationDate, insertUserID);
            }

        }
        public long Insert(DBDataContext db, string companyname, string companydescription, string location, int phone, string pobox, string generalmanager, int tillnumber,DateTime formationDate, string insertUserID)
        {
            //Create a new object 
            Donationlist_Reg TD = new Donationlist_Reg
            {
                companyname = companyname,
                companydescription = companydescription,
                location = location,
                phone = phone,
                pobox = pobox,              
                generalmanager = generalmanager,
                tillnumber = tillnumber,
                formationdate = DateTime.Now,
                InsertUserId = insertUserID,
                UpdateUserId = insertUserID,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.Donationlist_Regs.InsertOnSubmit(TD);
            //send changes to the database
            db.SubmitChanges();
            return (TD.id);
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long ID, string companyname, string companydescription, string location, int phone, string pobox, string generalmanager, int tillnumber,DateTime formationDate, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Update(db, ID, companyname, companydescription, location, phone, pobox, generalmanager, tillnumber,formationDate, insertUserID, updateUserID, entryDate, version);
            }
        }
        public bool Update(DBDataContext db, long ID, string companyname, string companydescription, string location, int phone, string pobox, string generalmanager, int tillnumber,DateTime formationDate, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            //Create a new object 
            Donationlist_Reg TD = new Donationlist_Reg
            {
                id = ID,
                companyname = companyname,
                companydescription = companydescription,
                location = location,
                phone = phone,
                pobox = pobox,
                generalmanager = generalmanager,
                tillnumber = tillnumber,
                formationdate = DateTime.Now,
                InsertUserId = insertUserID,
                UpdateUserId = updateUserID,
                UpdateDate = DateTime.Now,
                EntryDate = entryDate,
                Version = version,
            };
            //save the record to the object model
            db.Donationlist_Regs.Attach(TD, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Other Methods
        public bool IsDuplicateEntry(string companyname)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                int result = (from td in db.Donationlist_Regs
                              where td.companyname == companyname
                              select td).Count();
                return (result > 0);
            }
        }
        #endregion Other Methods
    }
}
