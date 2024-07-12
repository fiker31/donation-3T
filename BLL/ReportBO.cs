using BLL.Framework;
using System;
using DAL;
using DAL.Framework;
namespace BLL
{
    #region Report Selection
    #region Report
    #region ReportBO
    [Serializable()]
    public class PMSReportsBO : BaseBO
    {
        #region Properties
        public long ReportId { get; set; }
        public string ReportName { get; set; }
        public string FileName { get; set; }
        public string ObjectName { get; set; }
        public string Description { get; set; }
        public string SubReportObjectName { get; set; }
        public string SubReportMethodName { get; set; }
        public string CategoryId { get; set; }
        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //////Get the entity object from the DAL.
            ////UIReport eNTQReport = new PMSReportsData().Select(id);
            ////MapEntityToProperties(eNTQReport);
            ////return eNTQReport != null;
            return true;
        }
        public override bool Load(string id)
        {
            throw new NotImplementedException();
        }
        protected override void MapEntityToCustomProperties(DAL.Framework.IBaseEntity entity)
        {
            ////UIReport eNTQReport = (UIReport)entity;
            ////ReportId = eNTQReport.ReportId;
            ////ReportName = eNTQReport.ReportName;
            ////FileName = eNTQReport.FileName;
            ////ObjectName = eNTQReport.ObjectName;
            ////Description = eNTQReport.Description;
            ////SubReportObjectName = eNTQReport.SubReportObjectName;
            ////SubReportMethodName = eNTQReport.SubReportMethodName;
            ////CategoryId = eNTQReport.CategoryId;
        }
        protected override string GetDisplayText()
        {
            return ReportName;
        }
        #endregion Overrides
        #endregion ReportBO
        //public void MapEntityToProperties(UIReport entity)
        //{
        //    if (entity != null)
        //    {
        //        ReportId = entity.ReportId;
        //        CategoryId = entity.CategoryId;
        //        Description = entity.Description;
        //        ReportName = entity.ReportName;
        //    }
        //}
    }
    #region ReportBOList
    //public class MISReportsBOList : List<MISReportsBO>
    //{
    //    public void Load(string categoryId)
    //    {
    //        LoadFromList(new MISReportsData().SelectReports(categoryId));
    //    }
    //    #region Private Methods
    //    protected void LoadFromList(List<UIReport> rps)
    //    {
    //        foreach (UIReport rp in rps)
    //        {
    //            MISReportsBO newRP = new MISReportsBO();
    //            newRP.MapEntityToProperties(rp);
    //            this.Add(newRP);
    //        }
    //    }
    //    #endregion Private Methods
    //}
    [Serializable()]
    public class PMSReportsBOList : BaseBOList<PMSReportsBO>
    {
        #region Overrides
        public override void Load()
        {
            ////LoadFromList(new PMSReportsData().Select());
        }
        #endregion Overrides
        #region Private Methods
        ////private void LoadFromList(List<UIReport> eNTQReports)
        ////{
        ////    if (eNTQReports.Count > 0)
        ////    {
        ////        foreach (UIReport eNTQReport in eNTQReports)
        ////        {
        ////            PMSReportsBO newENTQReportBO = new PMSReportsBO();
        ////            newENTQReportBO.MapEntityToProperties(eNTQReport);
        ////            this.Add(newENTQReportBO);
        ////        }
        ////    }
        ////}
        #endregion Private Methods
        #region Public Methods
        public void Load(string categoryId)
        {
            ////LoadFromList(new PMSReportsData().SelectReports(categoryId));
        }
        #endregion Public Methods
    }
    #endregion ReportBOList
    #endregion Report
    #endregion Report Selection
    public static class ReportBO
    {
        public static object[] AceptedRequest(DateTime From, DateTime To, String Status, string BranchId, Int64 SectorId, Int64 AppStage)
        {
            return null;// ReportData.AcceptedRequst(From,To,Status,BranchId,SectorId,AppStage).ToArray();
        }
    }
}
