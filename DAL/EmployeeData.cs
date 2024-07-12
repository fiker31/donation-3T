using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL
{
    public class EmployeeData : BaseData<Employee>
    {
        #region Overrides
        public override List<Employee> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<Employee> TD = (from td in db.Employees
                                            select td).ToList();
                return TD;
            }
        }
        public override Employee Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var TD = (from td in db.Employees
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
        public override Employee Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            Employee TD = new Employee();
            TD.id = id;
            TD.Version = version;
            db.Employees.Attach(TD);
            db.Employees.DeleteOnSubmit(TD);
            db.SubmitChanges();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        #region Insert
        public long Insert(string connectionString, string Firstname, string Lastname, string Department, string Position, string insertUserID)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Insert(db, Firstname,Lastname, Department, Position, insertUserID);
            }

        }
        public long Insert(DBDataContext db, string Firstname, string Lastname, string Department, string Position, string insertUserID)
        {
            //Create a new object 
            Employee TD = new Employee
            {
                Firstname = Firstname,
                Lastname = Lastname,
                Department=Department,
                Position=Position,
                InsertUserId = insertUserID,
                UpdateUserId = insertUserID,
                UpdateDate = DateTime.Now,
                EntryDate = DateTime.Now,
            };
            //save the record to the object model
            db.Employees.InsertOnSubmit(TD);
            //send changes to the database
            db.SubmitChanges();
            return (TD.id);
        }
        #endregion Insert
        #region Update
        public bool Update(string connectionString, long ID, string Firstname, string Lastname, string Department, string Position,string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            using (DBDataContext db = new DBDataContext(connectionString))
            {
                return Update(db, ID, Firstname,Lastname, Department, Position,insertUserID, updateUserID, entryDate, version);
            }
        }
        public bool Update(DBDataContext db, long ID, string Firstname, string Lastname, string Department, string Position,string insertUserID, string updateUserID, DateTime entryDate, Binary version)
        {
            //Create a new object 
            Employee TD = new Employee
            {
                id = ID,
                Department=Department,
                Position=Position,
                Firstname = Firstname,
                Lastname = Lastname,
                InsertUserId = insertUserID,
                UpdateUserId = updateUserID,
                UpdateDate = DateTime.Now,
                EntryDate = entryDate,
                Version = version,
            };
            //save the record to the object model
            db.Employees.Attach(TD, true);
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
                int result = (from td in db.Employees
                              where td.Firstname == Firstname
                              select td).Count();
                return (result > 0);
            }
        }
        #endregion Other Methods
    }
}
