using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL;
using BLL.Framework;
public partial class Administration_OtherLogViewer : BaseInquiryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsValidateInput())
        {
            //if (rblDisplay.SelectedValue == "LH")
            //{
            LogInOutBOList list = new LogInOutBOList();
            list.LoadLogInHistory(chkUserHost.Checked ? txtUserHost.Text : "",
                chkUserId.Checked ? txtUserId.Text : "",
                Convert.ToDateTime(chkDateAfter.Checked ? txtDateAfter.Text : "01/01/1900"),
                Convert.ToDateTime(chkDateBefore.Checked ? txtDateBefore.Text : "01/01/1900"));
            gvQuery.DataSource = list;
            gvQuery.DataBind();
            //}
            //else
            //{
            //ProcessLogBOList list = new ProcessLogBOList();
            //if (chkDateAfter.Checked && chkDateBefore.Checked)
            //    list.LoadBetween(Convert.ToDateTime(txtDateAfter.Text), Convert.ToDateTime(txtDateBefore.Text));
            //else if (chkDateBefore.Checked)
            //    list.LoadBefore(Convert.ToDateTime(txtDateBefore.Text));
            //else if (chkDateAfter.Checked)
            //    list.LoadAfter(Convert.ToDateTime(txtDateAfter.Text));
            //else
            //    list.Load();
            //    gvQuery.DataSource = null;
            //    gvQuery.DataBind();
            //}
        }
    }
    private bool IsValidateInput()
    {
        EntValidationErrors validationErrors = new EntValidationErrors();
        if (chkDateAfter.Checked)
        {
            if (txtDateAfter.Text == "")
                validationErrors.Add("Enter the occured after date.");
            else if (!ValidateBO.IsDate(txtDateAfter.Text))
                validationErrors.Add("Invalid occured after date.");
        }
        if (chkDateBefore.Checked)
        {
            if (txtDateBefore.Text == "")
                validationErrors.Add("Enter the occured before date.");
            else if (!ValidateBO.IsDate(txtDateBefore.Text))
                validationErrors.Add("Invalid occured before date.");
        }
        if (validationErrors.Count() != 0)
        {
            ValidationErrorMessages1.ValidationErrors = validationErrors;
            return false;
        }
        return true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        btnView_Click(sender, e);
        if (gvQuery.Rows.Count != 0)
        {
            SaveGridData(gvQuery, "Otherlog_" + DateTime.Now.ToShortDateString() + ".csv");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Administration.aspx");
    }
    protected void chkDateAfter_CheckedChanged(object sender, EventArgs e)
    {
        txtDateAfter.Enabled = chkDateAfter.Checked;
    }
    protected void chkDateBefore_CheckedChanged(object sender, EventArgs e)
    {
        txtDateBefore.Enabled = chkDateBefore.Checked;
    }
    protected void chkUserId_CheckedChanged(object sender, EventArgs e)
    {
        txtUserId.Enabled = chkUserId.Checked;
    }
    protected void chkUserHost_CheckedChanged(object sender, EventArgs e)
    {
        txtUserHost.Enabled = chkUserHost.Checked;
    }
    protected void gvQuery_PageIndexChanged(object sender, EventArgs e)
    {
        btnView_Click(sender, e);
    }
    protected void gvQuery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvQuery.PageIndex = e.NewPageIndex;
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        IgnoreCapabilityCheck = true;
    }
    public override string MenuItemName()
    {
        return "Login History";
    }
    public override string[] CapabilityNames()
    {
        throw new NotImplementedException();
    }
}
