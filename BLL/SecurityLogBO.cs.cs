using System;
using System.Collections.Generic;
using DAL;
using DAL.Framework;
namespace BLL
{
    public class LogInOutBO
    {
        #region Properties
        public long Id { get; set; }
        public string UserId { get; set; }
        public DateTime LogInDate { get; set; }
        public DateTime LogOutDate { get; set; }
        public char Status { get; set; }
        public string ApplicationName { get; set; }
        public string UserHost { get; set; }
        #endregion Properties
        #region public Methods
        public long UpdateUserLogInHistory(string userAccountName, string userHostName)
        {
            return new SecurityLogsData().UpdateUserLogHistory(userAccountName, userHostName);
        }
        public bool UpdateLogOutTime(long logInOutId)
        {
            return new SecurityLogsData().UpdateLogOutTime(logInOutId);
        }
        public void MapEntityToProperties(LogInOut entity)
        {
            Id = entity.Id;
            UserId = entity.UserID;
            LogInDate = entity.LogInDT;
            LogOutDate = entity.LogOutDT;
            Status = entity.Status;
            UserHost = entity.UserHost;
        }
        #endregion Public Methods
    }
    public class LogInOutBOList : List<LogInOutBO>
    {
        #region Public Methods
        public void Load()
        {
            LoadFromList(new SecurityLogsData().SelectLOs());
        }
        public void LoadLogInHistory(string userHost, string userId, DateTime startDate, DateTime endDate)
        {
            LoadFromList(new SecurityLogsData().SelectLOs(userHost, userId, startDate, endDate));
        }
        #endregion Public Methods
        #region Private Methods
        private void LoadFromList(List<LogInOut> LNs)
        {
            foreach (LogInOut ln in LNs)
            {
                LogInOutBO newLBO = new LogInOutBO();
                newLBO.MapEntityToProperties(ln);
                this.Add(newLBO);
            }
        }
        #endregion Private Methods
    }
    #region Process Log
    //public class ProcessLogBO
    //{
    //    #region Properties
    //    public long Id { get; set; }
    //    public string ProcessName { get; set; }
    //    public string ProcessBy { get; set; }
    //    public DateTime EventDate { get; set; }
    //    public string Remark { get; set; }
    //    #endregion Properties
    //    #region public Methods
    //    public long UpdateProcessLog()
    //    {
    //        return new SecurityLogsData().UpdateProcessLog(ProcessBy, ProcessName, Remark);
    //    }
    //    public long UpdateProcessLog(string processBy, string processName, string remark)
    //    {
    //        return new SecurityLogsData().UpdateProcessLog(processBy, processName, remark);
    //    }
    //    public void MapEntityToProperties(ProcessLog entity)
    //    {
    //        Id = entity.Id;
    //        ProcessBy = entity.ProcessBy;
    //        ProcessName = entity.ProcessType;
    //        EventDate = entity.EventDate;
    //        Remark = entity.Remark;
    //    }
    //    #endregion Public Methods
    //}
    //public class ProcessLogBOList : List<ProcessLogBO>
    //{
    //    #region Public Methods
    //    public void Load()
    //    {
    //        LoadFromList(new SecurityLogsData().SelectPLs());
    //    }
    //    public void LoadBefore(DateTime beforeDate)
    //    {
    //        LoadFromList(new SecurityLogsData().SelectPLs(Convert.ToDateTime("01/01/1900"), beforeDate));
    //    }
    //    public void LoadAfter(DateTime startDate)
    //    {
    //        LoadFromList(new SecurityLogsData().SelectPLs(startDate, DateTime.Now));
    //    }
    //    public void LoadBetween(DateTime startDate, DateTime endDate)
    //    {
    //        LoadFromList(new SecurityLogsData().SelectPLs(startDate, endDate));
    //    }
    //    #endregion Public Methods
    //    #region Private Methods
    //    private void LoadFromList(List<ProcessLog> LNs)
    //    {
    //        foreach (ProcessLog ln in LNs)
    //        {
    //            ProcessLogBO newLBO = new ProcessLogBO();
    //            newLBO.MapEntityToProperties(ln);
    //            this.Add(newLBO);
    //        }
    //    }
    //    #endregion Private Methods
    //}
    #endregion Process Log
}
