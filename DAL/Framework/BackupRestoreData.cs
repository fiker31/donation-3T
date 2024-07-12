using System;
namespace DAL.Framework
{
    public class BackupRestoreData
    {
        public bool backupDB(string sqlStatment)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                try
                {
                    bool result = Convert.ToBoolean(db.ExecuteCommand(sqlStatment));
                    return result;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
