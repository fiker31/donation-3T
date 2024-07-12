using System.Text;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for BaseInquiryPage
/// </summary>
public abstract class BaseInquiryPage : BasePage
{
    public BaseInquiryPage()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void SaveGridData(GridView gvQuery, string csvFileName)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + csvFileName);
        Response.Charset = "";
        Response.ContentType = "application/text";
        //btnView_Click(sender, e);
        gvQuery.AllowPaging = false;
        gvQuery.DataBind();
        StringBuilder sb = new StringBuilder();
        string delimiter = "\"";
        //for (int k = 0; k < gvQuery.Columns.Count; k++)
        for (int k = 0; k < gvQuery.HeaderRow.Cells.Count; k++)
        {
            //add separator
            sb.Append(gvQuery.HeaderRow.Cells[k].Text + ',');
        }
        //append new line
        sb.Append("\r\n");
        for (int i = 0; i < gvQuery.Rows.Count; i++)
        {
            //for (int k = 0; k < gvQuery.Columns.Count; k++)
            for (int k = 0; k < gvQuery.Rows[i].Cells.Count; k++)
            {
                //add separator
                sb.Append(delimiter + gvQuery.Rows[i].Cells[k].Text + delimiter + ',');
            }
            //append new line
            sb.Append("\r\n");
        }
        Response.Output.Write(sb.ToString());
        Response.Flush();
        Response.End();
    }
}
