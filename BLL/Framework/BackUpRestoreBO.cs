using DAL.Framework;

namespace BLL.Framework
{
    public class BackUpRestoreBO
    {
        public bool BackUp(string sqlStatment)
        {
            return new BackupRestoreData().backupDB(sqlStatment);
        }
    }
}
