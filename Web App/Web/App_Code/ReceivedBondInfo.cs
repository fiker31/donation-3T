/// <summary>
/// Summary description for RequestInfo
/// </summary>
public class RequestInfo
{
    public RequestInfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public long Id { get; set; }
    public string CustomerName { get; set; }
    public decimal AmountRequested { get; set; }
    public decimal AmountApproved { get; set; }
    public string Relationship { get; set; }
    public string subsector { get; set; }
    public string CommodityDescription { get; set; }
    public bool chkMark { get; set; }
    public char ApprovalType { get; set; }
}
