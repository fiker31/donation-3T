using DAL;
using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL
{
    public class BranchData : BaseData<Branch>
    {
        #region Overrides
        public override List<Branch> Select()
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Branch> TD = (from td in db.Branches
                                   select td).ToList();
                return TD;
            }
        }
        public override Branch Select(long id)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.Branches
                          where td.ID == id
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
        public override Branch Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(AppDataContext db, long id, Binary version)
        {
            Branch TD = new Branch();
            TD.ID = id;
            TD.Version = version;
            db.Branches.Attach(TD);
            db.Branches.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }
        public override void Delete(AppDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, string Name, long districtID, string T24Code, string Address, string Contact, string insertUserId)
        {
            using (AppDataContext db = new AppDataContext(connectionString))
            {
                return Insert(db, Name, districtID, T24Code, Address, Contact, insertUserId);
            }
        }
        public long Insert(AppDataContext db, string Name, long districtID, string T24Code, string Address, string Contact, string insertUserId)
        {
            //Create a new object 
            Branch TD = new Branch
            {
                Name = Name,
                DistrictID = districtID,
                T24Code = T24Code,
                Address = Address,
                Contact = Contact,
                InsertUserId = insertUserId,
                UpdateUserId = insertUserId,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.Branches.InsertOnSubmit(TD);
            //send changes to the database
            db.SubmitChanges();
            return (TD.ID);
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long Id, string Name, long districtID, string T24Code, string Address, string Contact, string insertUserId, string updateUserId, DateTime entryDate, Binary version)
        {
            using (AppDataContext db = new AppDataContext(connectionString))
            {
                return Update(db, Id, Name, districtID, T24Code, Address, Contact, insertUserId, updateUserId, entryDate, version);
            }
        }
        public bool Update(AppDataContext db, long Id, string Name, long districtID, string T24Code, string Address, string Contact, string insertUserId, string updateUserId, DateTime entryDate, Binary version)
        {
            //Create a new object 
            Branch TD = new Branch
            {
                ID = Id,
                Name = Name,
                DistrictID = districtID,
                T24Code = T24Code,
                Address = Address,
                Contact = Contact,
                InsertUserId = insertUserId,
                UpdateUserId = updateUserId,
                UpdateDate = DateTime.Now,
                EntryDate = entryDate,
                Version = version,
            };
            //save the record to the object model
            db.Branches.Attach(TD, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Other Methods
        public List<Branch> SelectByBranchName(string Name)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Branch> TD = (from td in db.Branches
                                   where td.Name.StartsWith(Name)
                                   select td).ToList();
                return TD;
            }
        }
        public bool IsDuplicateEntry(string Name)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                int result = (from td in db.Branches
                              where td.Name == Name
                              select td).Count();
                return (result > 0);
            }
        }
        #endregion Other Methods
    }
}
