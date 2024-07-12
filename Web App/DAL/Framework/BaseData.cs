using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL.Framework
{
    public abstract class BaseData<T> where T : IBaseEntity
    {
        private const string CONNECTION_STRING_KEY = "ConnectionString";  //GymDBConnectionString
        public abstract List<T> Select();
        public abstract T Select(long id);
        public abstract T Select(string id);
        public abstract void Delete(AppDataContext db, long id, Binary version);
        public abstract void Delete(AppDataContext db, string id, Binary version);
        public void Delete(string connectionString, long id, Binary version)
        {
            using (AppDataContext db = new AppDataContext(connectionString))
            {
                Delete(db, id, version);
            }
        }
        public void Delete(string connectionString, string id, Binary version)
        {
            using (AppDataContext db = new AppDataContext(connectionString))
            {
                Delete(db, id, version);
            }
        }
        //Updates the Primary Key Value for table.
        protected bool UpdateKey_Value(string connectionString, string SourceEntity, string OldValue, string NewValue)
        {
            using (AppDataContext db = new AppDataContext(connectionString))
            {
                return (db.UpdatePrimaryKey(SourceEntity, OldValue, NewValue) == 1);
            }
        }
        protected bool UpdateKey_Value(AppDataContext db, string SourceEntity, string OldValue, string NewValue)
        {
            return (db.UpdatePrimaryKey(SourceEntity, OldValue, NewValue) == 1);
        }
        #region Common Functions
        protected static bool IsDuplicate(AppDataContext db, string tableName, string fieldName,
            string fieldNameId, string value, long id)
        {
            string sql =
                "SELECT COUNT(" + fieldNameId + ") AS DuplicateCount " +
                  "FROM " + tableName +
                " WHERE " + fieldName + " = {0}" +
                  " AND " + fieldNameId + " <> {1}";
            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { value, id });
            List<DuplicateCheck> list = result.ToList();
            return (list[0].DuplicateCount > 0);
        }
        protected static bool IsDuplicate(AppDataContext db, string tableName, string fieldName,
            string fieldNameId, DateTime value, long id)
        {
            string sql =
                "SELECT COUNT(" + fieldNameId + ") AS DuplicateCount " +
                  "FROM " + tableName +
                " WHERE " + fieldName + " = {0}" +
                  " AND " + fieldNameId + " <> {1}";
            var result = db.ExecuteQuery<DuplicateCheck>(sql, new object[] { value, id });
            List<DuplicateCheck> list = result.ToList();
            return (list[0].DuplicateCount > 0);
        }
        #endregion Common Functions
    }
}
