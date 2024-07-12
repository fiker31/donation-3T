using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DAL
{
    public class SecurityLogsData
    {
        public long UpdateUserLogHistory(string userAccountName, string userHost)
        {
            //Create a new User Account object
            LogInOut L = new LogInOut
            {
                UserID = userAccountName,
                LogInDT = DateTime.Now,
                LogOutDT = DateTime.Now,
                Status = 'I',
                UserHost = userHost
            };
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                //save the record to the object model
                db.LogInOuts.InsertOnSubmit(L);
                //send changes to the database
                db.SubmitChanges();
            }
            return L.Id;
        }
        public bool UpdateLogOutTime(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                db.ExecuteCommand("Update LogInOut Set LogOutDT = GetDate(), Status= 'O' WHERE [Id] =" + id.ToString());
            }
            return true;
        }
        public List<LogInOut> SelectLOs(string userHost, string userId, DateTime fromDate, DateTime toDate)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var L = (from l in db.LogInOuts
                         where fromDate.ToShortDateString() != "01/01/1900" ? l.LogInDT >= fromDate : true &&
                         toDate.ToShortDateString() != "01/01/1900" ? l.LogInDT <= toDate : true &&
                         userHost != "" ? l.UserHost.StartsWith(userHost) : true &&
                         userId != "" ? l.UserID.StartsWith(userId) : true
                         select l).ToList();
                return L;
            }
        }
        public List<LogInOut> SelectLOs()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var L = (from l in db.LogInOuts
                         select l).ToList();
                return L;
            }
        }
        //public long UpdateProcessLog(string userAccountName,string processType,string remark)
        //{
        //    //Create a new User Account object
        //    ProcessLog P = new ProcessLog
        //    {
        //        ProcessBy = userAccountName,
        //        EventDate = DateTime.Now,
        //        ProcessType = processType,
        //        Remark = remark
        //    };
        //    using (SMSDataContext db = new SMSDataContext(DBHelper.GetCreditDBConnectionString()))
        //    {
        //        //save the record to the object model
        //        db.ProcessLogs.InsertOnSubmit(P);
        //        //send changes to the database
        //        db.SubmitChanges();
        //    }
        //    return P.Id;
        //}
        //public List<ProcessLog> SelectPLs(DateTime fromDate, DateTime toDate)
        //{
        //    using (SMSDataContext db = new SMSDataContext(DBHelper.GetCreditDBConnectionString()))
        //    {
        //        var PL = (from pl in db.ProcessLogs
        //                   where pl.EventDate >= fromDate &&
        //                   pl.EventDate<=toDate 
        //                   select pl).ToList();
        //        return PL;
        //    }
        //}
        //public List<ProcessLog> SelectPLs()
        //{
        //    using (SMSDataContext db = new SMSDataContext(DBHelper.GetCreditDBConnectionString()))
        //    {
        //        var PL = (from pl in db.ProcessLogs
        //                  select pl).ToList();
        //        return PL;
        //    }
        //}
    }
}
