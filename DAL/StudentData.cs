using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL
{
    public class StudentData : BaseData<Student>
    {
        #region Overrides
        public override List<Student> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Student> TD = (from td in db.Students
                                            select td).ToList();
                return TD;
            }
        }
        public override Student Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.Students
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
        public override Student Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            Student TD = new Student();
            TD.Id = id;
            TD.Version = version;
            db.Students.Attach(TD);
            db.Students.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, string Firstname, string Lastname, string insertUserID)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Insert(db, Firstname,Lastname, insertUserID);
            }

        }
        public long Insert(DBDataContext db, string Firstname, string Lastname, string insertUserID)
        {
            //Create a new object 
            Student TD = new Student
            {
                Firstname = Firstname,
                Lastname = Lastname,
                InsertUserId = insertUserID,
                UpdateUserId = insertUserID,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.Students.InsertOnSubmit(TD);
            //send changes to the database
            db.SubmitChanges();
            return (TD.Id);
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long ID, string Firstname, string Lastname, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Update(db, ID, Firstname,Lastname, insertUserID, updateUserID, entryDate, version);
            }
        }
        public bool Update(DBDataContext db, long ID, string Firstname, string Lastname, string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            //Create a new object 
            Student TD = new Student
            {
                Id = ID,
                Firstname = Firstname,
                Lastname = Lastname,
                InsertUserId = insertUserID,
                UpdateUserId = updateUserID,
                UpdateDate = DateTime.Now,
                EntryDate = entryDate,
                Version = version,
            };
            //save the record to the object model
            db.Students.Attach(TD, true);
            //send changes to the database
            db.SubmitChanges();
            return true;
        }
        #endregion Update
        #region Other Methods
        public bool IsDuplicateEntry(string Firstname)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                int result = (from td in db.Students
                              where td.Firstname == Firstname
                              select td).Count();
                return (result > 0);
            }
        }
        #endregion Other Methods
    }
}
