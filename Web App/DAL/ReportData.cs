using System;
namespace DAL
{
    #region Types
    public class AceptedRequest
    {
        public string ID { get; set; }
        public string Queue { get; set; }
        public string ImporterName { get; set; }
        public string NBEAccount { get; set; }
        public string TIN { get; set; }
        public string ProfInvoiceNumber { get; set; }
        public string Currency { get; set; }
        public string ProfInvAmount { get; set; }
        public string ProfInvliceAmountUSD { get; set; }
        public string Commodity { get; set; }
        public string PaymentMode { get; set; }
        public string Sector { get; set; }
        public string Branch { get; set; }
        public string RequestDate { get; set; }
        public string Status { get; set; }
        public Int64 CurrentQueue { get; set; }
    }
    #endregion
    #region  Report Data
    public static class ReportData
    {
        //public static List<AceptedRequest> AcceptedRequst(DateTime From, DateTime To,String Status,string BranchId,Int64 SectorId,Int64 AppStage)
        //{
        //    using (SMSDataContext db = new SMSDataContext(DBHelper.GetCreditDBConnectionString()))
        //    {
        //        List<AceptedRequest> IER = (from td in db.AcceptedRequests
        //                                    where (Status.Trim()=="ALL" ? true: td.Status.ToString() == Status)
        //                                    && (BranchId=="0"? true:td.BranchId.ToString() == BranchId.ToString()) &&
        //                                    (SectorId==0?true:td.SectorId==SectorId) &&
        //                                    (AppStage==4?true:td.ApprovalStage==AppStage) &&
        //                                    Convert.ToDateTime(td.RequestEntryDate.Value.Date).CompareTo(From.Date)>=0 &&
        //                                    Convert.ToDateTime(td.RequestEntryDate.Value.Date).CompareTo(To.Date) <= 0
        //                                    select new AceptedRequest
        //                                    {
        //                                        ID = td.Id.ToString(),
        //                                        Branch = td.BranchName,
        //                                        Commodity = td.TypeOfCommodity,
        //                                        Currency = td.CurrencyName,
        //                                        ImporterName = td.ImporterName,
        //                                        NBEAccount = td.NBEAcc,
        //                                        PaymentMode = td.PaymentMode,
        //                                        ProfInvAmount = td.ProfInvAmount.ToString(),
        //                                        ProfInvliceAmountUSD = td.ProfInvAmountUSD.ToString(),
        //                                        ProfInvoiceNumber = td.ProfInvNo,
        //                                        RequestDate = td.RequestEntryDate.ToString(),
        //                                        Sector = td.SectorName,
        //                                        Status =new RequestData().ConvertNumber(Convert.ToInt64(td.ApprovalStage)),
        //                                        TIN = td.TINumber,
        //                                        Queue=td.Queue.ToString(),
        //                                        CurrentQueue=new RequestData().GetCurrentQueue(td.Id),
        //                                    }).ToList();
        //        return IER;
        //    }
        //}
    }
    #endregion
}
