using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.VisualBasic;
using PMSDAL.Framework;
using PMSDAL;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace PMSDAL.Reports
{
    #region Report Selection
    public class PMSReportsData : BaseData<UIReport>
    {
        public List<UIReport> SelectReports(String categoryId)
        {
            using (PMSDataContext db = new PMSDataContext(DBHelper.GetPMSDBConnectionString()))
            {

                List<UIReport> UIR = (from uir in db.UIReports
                                      where uir.CategoryId == categoryId
                                      orderby uir.Description
                                      select uir).ToList();
                return UIR;
            }
        }

        public List<UIReportCategory> SelectCategories(char reportClass, char outputType)
        {
            using (PMSDataContext db = new PMSDataContext(DBHelper.GetPMSDBConnectionString()))
            {

                List<UIReportCategory> RC = (from rc in db.UIReportCategories
                                             where rc.OType == outputType && rc.CategoryId.StartsWith(Convert.ToString(reportClass))
                                             orderby rc.Description
                                             select rc).ToList();
                return RC;
            }
        }

        public override List<UIReport> Select()
        {
            using (PMSDataContext db = new PMSDataContext(DBHelper.GetPMSDBConnectionString()))
            {
                List<UIReport> QR = (from qr in db.UIReports
                                     select qr).ToList();
                return QR;

            }
        }

        public override UIReport Select(long id)
        {
            using (PMSDataContext db = new PMSDataContext(DBHelper.GetPMSDBConnectionString()))
            {

                var QR = (from qr in db.UIReports
                          where qr.ReportId == id
                          select qr);
                if (QR.Count() != 0)
                {
                    return QR.Single();
                }
                else
                {
                    return null;
                }
            }
        }

        public override UIReport Select(string id)
        {
            throw new NotImplementedException();
        }

        public override void Delete(PMSDataContext db, long id, Binary version)
        {
            UIReport QR = new UIReport();

            QR.ReportId = id;
            QR.Version = version;
            db.UIReports.Attach(QR);

            db.UIReports.DeleteOnSubmit(QR);
            db.SubmitChanges();
        }

        public override void Delete(PMSDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }

    }

    #endregion Report Selection

    #region Report
    #region Types

    public class IndividualReport
    {

        public string Project { get; set; }
        public string Tasks { get; set; }
        public string AssignmentTitle { get; set; }
        public string ReportDetail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string OtherIssues { get; set; }
        public double TimeElapsed { get; set; }
        public string Performer { get; set; }
        public int Achieved { get; set; }
        public string ProjectType { get; set; }
        public DateTime RequestDate { get; set; }
    }

    public class ProjectReport
    {

        public string Project { get; set; }
        public string Tasks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Report { get; set; }
        public string Status { get; set; }
        public int PercentAchived { get; set; }
        public string Remark { get; set; }
        public string IssuesRequired { get; set; }
        public double TimeElapsed { get; set; }
    }
    public class TeamReport
    {

        public string Project { get; set; }
        public string Tasks { get; set; }
        public string ReportDetail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string OtherIssues { get; set; }
        public double TimeElapsed { get; set; }
        public int Achieved { get; set; }
    }
    public class ProjectStatus
    {

        public string Project { get; set; }
        public string MajorTasks { get; set; }
        public DateTime PStartTime { get; set; }
        public DateTime PEndTime { get; set; }
        public DateTime AStartTime { get; set; }
        public DateTime AEndTime { get; set; }
        public string Status { get; set; }
        public double VStartDate { get; set; }
        public double VEndDate { get; set; }
        
    }

    public class TeamMembers
    {
        public long projectId { get; set; }
        public string Member { get; set; }
    }

    public class project
    {
        public string Request { get; set; }
        public string Owner { get; set; }
        public string ContactPerson { get; set; }
        public string ProjectType { get; set; }
        public long projectId { get; set; }
        public string projectName { get; set; }
        public string TeamLeader { get; set; }
        public char ProjectStatus { get; set; }
        public string WorkOrder { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanEndDate { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public char Status { get; set; }
    }

    #endregion Types

    #region  Report Data
    public static class ReportData
    {


        public static List<project> project(long projectId)
        {
            using (PMSDataContext db = new PMSDataContext(DBHelper.GetPMSDBConnectionString()))
            {
                List<project> IER = (from r in db.Requests
                                              join p in db.Projects
                                              on r.Id equals p.RequestId
                                              join pt in db.ProjectTypes
                                              on r.RequestTypeId equals pt.Id
                                              join pp in db.ProjectWorkOrderPlans
                                              on p.Id equals pp.ProjectId
                                              join u in db.EntUserAccounts
                                              on p.TeamLeaderId equals u.UserAccountId
                                              join w in db.WorkOrders
                                              on pp.WorkOrderId equals w.Id

                                              where p.Id == projectId

                                              select new project
                                              {
                                                  Request = r.Title,
                                                  Owner = r.Customer,
                                                  ContactPerson = r.ContactPerson,
                                                  ProjectType = pt.Description,
                                                  projectId = p.Id,
                                                  projectName = p.ProjectName,
                                                  TeamLeader = u.FirstName + " " + u.LastName,
                                                  ProjectStatus = p.ProjectStatus,
                                                  WorkOrder = w.Description,
                                                  PlanStartDate = pp.PlanStartDate,
                                                  PlanEndDate = pp.PlanEndDate,
                                                  ActualStartDate = pp.ActualStartDate,
                                                  ActualEndDate = pp.ActualEndDate,
                                                  Status = pp.Status


                                              }).ToList();
                return IER;

            }
        }

        public static List<TeamMembers> TeamMembers(long projectId)
        {
            using (PMSDataContext db = new PMSDataContext(DBHelper.GetPMSDBConnectionString()))
            {
                List<TeamMembers> IER = (from  p in db.Projects
                                         join tm in db.ProjectTeamMembers
                                         on p.Id equals tm.ProjectId
                                        join u in db.EntUserAccounts
                                        on tm.TeamMemberId equals u.UserAccountId
                                     where tm.ProjectId == projectId


                                     select new TeamMembers
                                     {
                                         projectId = p.Id,
                                         Member = u.FirstName + " " + u.LastName,
                                     }).ToList();
                return IER;

            }
        }

        public static List<IndividualReport> IndividualReport(DateTime DateFrom, DateTime DateTo, long performer, long projectId)
        {
            using (PMSDataContext db = new PMSDataContext(DBHelper.GetPMSDBConnectionString()))
            {

                string sql = " SELECT     dbo.SoftwareApplication.ApplicationDescription AS Project, '' AS Task, dbo.Project.ProjectName AS AssignmentTitle, " +
                        "dbo.MaintenanceReport.TaskDetail AS ReportDetail, Project.StartDate, dbo.MaintenanceReport.EntryDate as EndDate, " +
                        "dbo.Project.ProjectStatus AS Status, case when Justification is not null then Justification + ' ' + dbo.MaintenanceReport.Remark else dbo.MaintenanceReport.Remark end as Remark, '' AS OtherIssues, " +
                        "Case when Project.StartDate <> Convert(datetime,'01/01/9999',103) then isnull( dbo.GetWorkingDays(Project.StartDate,dbo.MaintenanceReport.EntryDate) -  dbo.getHoliday(Project.StartDate,dbo.MaintenanceReport.EntryDate),0.00) else 0 end  AS TimeElapsed, " +
                        "dbo.EntUserAccount.FirstName + ' ' + dbo.EntUserAccount.LastName AS Performer, 0 AS Achived , dbo.ProjectType.Description As ProjectType,RequestDate  " +
                        "FROM dbo.ProjectTeamMember INNER JOIN  " +
                      "dbo.Project ON dbo.ProjectTeamMember.ProjectId = dbo.Project.Id INNER JOIN  " +
                      "dbo.EntUserAccount ON dbo.ProjectTeamMember.TeamMemberId = dbo.EntUserAccount.UserAccountId INNER JOIN  " +
                      "dbo.MaintenanceReport ON dbo.EntUserAccount.UserAccountId = dbo.MaintenanceReport.PerformerId AND  " +  
                      "dbo.Project.Id = dbo.MaintenanceReport.RequestId INNER JOIN  " +
                      "dbo.RequestDecision ON dbo.Project.RequestId = dbo.RequestDecision.Id INNER JOIN  " +
                      "dbo.Request ON dbo.RequestDecision.RequestId = dbo.Request.Id INNER JOIN  " +
                      "dbo.SoftwareApplication ON dbo.Request.ApplicationId = dbo.SoftwareApplication.Id   INNER JOIN " +
                      "dbo.ProjectType ON dbo.Request.RequestTypeId = dbo.ProjectType.Id "+
                      "LEFT OUTER JOIN dbo.DelayJustification ON dbo.ProjectTeamMember.ProjectId = dbo.DelayJustification.RequestId AND " +
                       "dbo.ProjectTeamMember.TeamMemberId = dbo.DelayJustification.PerformerId " +
                      "Where (dbo.MaintenanceReport.EntryDate between Convert(datetime,'" + DateFrom + "',103) AND  Convert(datetime,'" + DateTo + "',103)) AND " +
                      "UserAccountId = '" + performer + "'";

                sql = sql + "UNION SELECT  dbo.Request.Title AS Project, '' AS Task, dbo.Project.ProjectName AS AssignmentTitle, dbo.OtherReport.ReportDetail, " +
                        "Project.StartDate,OtherReport.EntryDate  as EndDate, dbo.Project.ProjectStatus AS Status, case when Justification is not null then Justification + ' ' + dbo.OtherReport.Remark else dbo.OtherReport.Remark end as Remark, '' AS OtherIssues, " +
                        "Case when Project.StartDate <> Convert(datetime,'01/01/9999',103) then isnull( dbo.GetWorkingDays(Project.StartDate,OtherReport.EntryDate ) -  dbo.getHoliday(Project.StartDate,OtherReport.EntryDate ),0.00) else 0 end AS TimeElapsed, " +
                        "dbo.EntUserAccount.FirstName + ' ' + dbo.EntUserAccount.LastName AS Performer, 0 AS Achived, dbo.ProjectType.Description As ProjectType,RequestDate " +
                        "FROM dbo.EntUserAccount INNER JOIN " +
                        "dbo.OtherReport ON dbo.EntUserAccount.UserAccountId = dbo.OtherReport.PerformerId INNER JOIN " +
                        "dbo.Project ON dbo.EntUserAccount.UserAccountId = dbo.Project.TeamLeaderId  AND dbo.OtherReport.AssignmentId = dbo.Project.Id INNER JOIN " +
                        "dbo.RequestDecision ON dbo.Project.RequestId = dbo.RequestDecision.Id INNER JOIN " +
                        "dbo.Request ON dbo.RequestDecision.RequestId = dbo.Request.Id INNER JOIN " +
                        "dbo.ProjectType ON dbo.Request.RequestTypeId = dbo.ProjectType.Id " +
                        "INNER JOIN     dbo.ProjectTeamMember ON dbo.EntUserAccount.UserAccountId = dbo.ProjectTeamMember.TeamMemberId AND " +
                        "dbo.Project.Id = dbo.ProjectTeamMember.ProjectId LEFT OUTER JOIN dbo.DelayJustification ON " +
                        "dbo.ProjectTeamMember.ProjectId = dbo.DelayJustification.RequestId AND dbo.ProjectTeamMember.TeamMemberId = dbo.DelayJustification.PerformerId " +
                        "Where (OtherReport.EntryDate between Convert(datetime,'" + DateFrom + "',103) AND  Convert(datetime,'" + DateTo + "',103)) AND " +
                        "UserAccountId = " + performer ;

                sql = sql + "UNION SELECT     dbo.Project.ProjectName AS Project, dbo.WorkOrder.Description AS Tasks, dbo.Assignment.AssignmentTitle AS AssignmentTitle, " +
                      " dbo.IndividualReport.ReportDetail AS ReportDetail, Assignment.StartDate AS StartDate, Assignment.EndDate AS EndDate, " +
                      "dbo.IndividualReport.ReportStauts AS Status, dbo.IndividualReport.Remark AS Remark, dbo.IndividualReport.OtherIssues AS OtherIssues,  " +
                      "Case when Assignment.EndDate < Assignment.entrydate then isnull( dbo.GetWorkingDays(Assignment.StartDate,Assignment.entrydate )-  dbo.getHoliday(Assignment.StartDate,Assignment.entrydate),0.00) else 0 end  AS TimeElapsed, " +
                      "dbo.EntUserAccount.FirstName + ' ' + dbo.EntUserAccount.LastName AS Performer, 0 AS Achived, dbo.ProjectType.Description As ProjectType,Assignment.StartDate as RequestDate " +
                      "FROM dbo.Project INNER JOIN dbo.ProjectWorkOrderPlan ON dbo.Project.Id = dbo.ProjectWorkOrderPlan.ProjectId INNER JOIN " +
                      "dbo.WorkOrder ON dbo.ProjectWorkOrderPlan.WorkOrderId = dbo.WorkOrder.Id INNER JOIN " +
                      "dbo.Assignment ON dbo.Project.Id = dbo.Assignment.RequestId INNER JOIN " +
                      "dbo.IndividualReport ON dbo.Assignment.Id = dbo.IndividualReport.AssignmentId AND " +
                      "dbo.WorkOrder.Id = dbo.IndividualReport.WorkOrderID INNER JOIN " +
                      "dbo.EntUserAccount ON dbo.IndividualReport.UserId = dbo.EntUserAccount.UserAccountId INNER JOIN " +                     
                      "dbo.RequestDecision ON dbo.Project.RequestId = dbo.RequestDecision.Id INNER JOIN dbo.Request ON dbo.RequestDecision.RequestId = dbo.Request.Id INNER JOIN " +
                      "dbo.ProjectType ON dbo.Request.RequestTypeId = dbo.ProjectType.Id " +
                      "Where (ReportTo between Convert(datetime,'" + DateFrom + "',103) AND  Convert(datetime,'" + DateTo + "',103)) ";
                if (performer != 0)
                    sql = sql + " AND UserAccountId = " + performer ;
                if (projectId != 0)
                    sql = sql + " AND dbo.Project.Id = " + projectId;

                

                var result = db.ExecuteQuery<IndividualReport>(sql);
                List<IndividualReport> PS = result.ToList();

                return PS;
            }

        }

        public static List<ProjectReport> ProjectTeamReport(DateTime DateFrom, DateTime DateTo,  long projectId)
        {
            using (PMSDataContext db = new PMSDataContext(DBHelper.GetPMSDBConnectionString()))
            {
                string sql = "SELECT ProjectName AS Project, dbo.WorkOrder.Description AS Tasks, PlanStartDate as StartDate,PlanEndDate as EndDate, " +
                             "Report, dbo.ProjectTeamReport.Status, PercentAchived, dbo.ProjectTeamReport.Remark, IssuesRequired, " +
                             "Case when ProjectWorkOrderPlan.Status = 'I'  then isnull(dbo.GetWorkingDays(PlanEndDate,getdate() ) - dbo.getHoliday(PlanEndDate,getdate()),0.00) " +
                             "else isnull(dbo.GetWorkingDays(PlanEndDate,ActualEndDate) - dbo.getHoliday(PlanEndDate,ActualEndDate),0.00) end AS TimeElapsed " +
                             "FROM dbo.Project INNER JOIN dbo.ProjectWorkOrderPlan ON dbo.Project.Id = dbo.ProjectWorkOrderPlan.ProjectId INNER JOIN " +
                             "dbo.WorkOrder ON dbo.ProjectWorkOrderPlan.WorkOrderId = dbo.WorkOrder.Id INNER JOIN " +
                             "dbo.ProjectTeamReport ON dbo.Project.Id = dbo.ProjectTeamReport.ProjectId AND dbo.WorkOrder.Id = dbo.ProjectTeamReport.workorderId " +
                             "Where ReportDateTo  between Convert(datetime,'" + DateFrom + "',103) AND  Convert(datetime,'" + DateTo + "',103) ";
                
                if (projectId != 0)
                    sql = sql + " AND dbo.Project.Id = " + projectId;
                
                var result = db.ExecuteQuery<ProjectReport>(sql);
                List<ProjectReport> PS = result.ToList();

                return PS;
            }

        }

        public static List<ProjectStatus> ProjectStatus(long projectId)
        {
            //"SELECT ProjectName AS Project, WorkOrder.Description AS MajorTasks, PlanStartDate AS PStartTime, " +
            //                 "PlanEndDate AS PEndTime,ActualStartDate AS AStartTime,ActualEndDate AS AEndTime,Status AS status,  " +
            //                 "'VStartDate' = case when ActualStartDate = '01/01/9999' then   isnull(dbo.GetWorkingDays(PlanStartDate,getdate() ) - dbo.getHoliday(PlanStartDate,getdate() ),0.00) " +
            //                 "else  isnull(dbo.GetWorkingDays(PlanStartDate,ActualStartDate ) -  dbo.getHoliday(PlanStartDate,ActualStartDate ),0.00) end , " +
            //                 "'VEndDate' = case when ActualEndDate = '01/01/9999'  then  isnull(dbo.GetWorkingDays(PlanEndDate,getdate() ) - dbo.getHoliday(PlanStartDate,getdate()),0.00) " +
            //                 "else  isnull(dbo.GetWorkingDays(ActualEndDate,getdate() ) - dbo.getHoliday(PlanStartDate,ActualStartDate ),0.00)end " +
            //                 "FROM dbo.Project INNER JOIN dbo.ProjectWorkOrderPlan ON dbo.Project.Id = dbo.ProjectWorkOrderPlan.ProjectId INNER JOIN " +
            //                 "dbo.WorkOrder ON dbo.ProjectWorkOrderPlan.WorkOrderId = dbo.WorkOrder.Id";
            using (PMSDataContext db = new PMSDataContext(DBHelper.GetPMSDBConnectionString()))
            {
                string sql = "SELECT ProjectName AS Project, WorkOrder.Description AS MajorTasks, PlanStartDate AS PStartTime, " +
                             "PlanEndDate AS PEndTime,ActualStartDate AS AStartTime,ActualEndDate AS AEndTime,Status AS status,  " +
                             "'VStartDate' = case when ActualStartDate = '01/01/9999' then   isnull(dbo.GetWorkingDays(PlanStartDate,getdate() ) ,0.00) "  +
                             "else  isnull(dbo.GetWorkingDays(PlanStartDate,ActualStartDate ),0.00) end , " +
                             "'VEndDate' = case when ActualEndDate = '01/01/9999'  then  isnull(dbo.GetWorkingDays(PlanEndDate,getdate() ) ,0.00) " +
                             "else  isnull(dbo.GetWorkingDays(PlanEndDate,ActualEndDate ) ,0.00)end " +
                             "FROM dbo.Project INNER JOIN dbo.ProjectWorkOrderPlan ON dbo.Project.Id = dbo.ProjectWorkOrderPlan.ProjectId INNER JOIN " +
                             "dbo.WorkOrder ON dbo.ProjectWorkOrderPlan.WorkOrderId = dbo.WorkOrder.Id";
                if (projectId != 0)
                    sql = sql + " AND dbo.Project.Id = " + projectId;

                var result = db.ExecuteQuery<ProjectStatus>(sql);
                List<ProjectStatus> PS = result.ToList();

                return PS;
            }

        }

        
    }

    #endregion Report Data
    #endregion Report
}
