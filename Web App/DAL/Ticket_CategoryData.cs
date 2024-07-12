using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL
{
    public class Ticket_CategoryData : BaseData<Ticket_Category>
    {
        #region Overrides
        public override List<Ticket_Category> Select()
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Ticket_Category> TD = (from td in db.Ticket_Categories
                                            select td).ToList();
                return TD;
            }
        }
        public override Ticket_Category Select(long id)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.Ticket_Categories
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
        public override Ticket_Category Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(AppDataContext db, long id, Binary version)
        {
            Ticket_Category TD = new Ticket_Category();
            TD.ID = id;
            TD.Version = version;
            db.Ticket_Categories.Attach(TD);
            db.Ticket_Categories.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }
        public override void Delete(AppDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, string CategoryName, string insertUserID)
        {
            using (AppDataContext db = new AppDataContext(connectionString))
            {
                return Insert(db, CategoryName, insertUserID);
            }
        }
        public long Insert(AppDataContext db, string CategoryName, string insertUserID)
        {
            //Create a new object 
            Ticket_Category TD = new Ticket_Category
            {
                CategoryName = CategoryName,
                InsertUserId = insertUserID,
                UpdateUserId = insertUserID,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.Ticket_Categories.InsertOnSubmit(TD);
            //send changes to the database
            db.SubmitChanges();
            return (TD.ID);
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long ID, string ProTemplateName, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            using (AppDataContext db = new AppDataContext(connectionString))
            {
                return Update(db, ID, ProTemplateName, insertUserID, updateUserID, entryDate, version);
            }
        }
        public bool Update(AppDataContext db, long ID, string CategoryName, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            //Create a new object 
            Ticket_Category TD = new Ticket_Category
            {
                ID = ID,
                CategoryName = CategoryName,
                InsertUserId = insertUserID,
                UpdateUserId = updateUserID,
                UpdateDate = DateTime.Now,
                EntryDate = entryDate,
                Version = version,
            };
            //save the record to the object model
            db.Ticket_Categories.Attach(TD, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Other Methods
        public List<Ticket_Category> SelectByCategoryName(string CategoryName)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Ticket_Category> TD = (from td in db.Ticket_Categories
                                            where td.CategoryName.StartsWith(CategoryName)
                                            select td).ToList();
                return TD;
            }
        }
        public bool IsDuplicateEntry(string CategoryName)
        {
            using (AppDataContext db = new AppDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                int result = (from td in db.Ticket_Categories
                              where td.CategoryName == CategoryName
                              select td).Count();
                return (result > 0);
            }
        }
        #endregion Other Methods
    }
}
