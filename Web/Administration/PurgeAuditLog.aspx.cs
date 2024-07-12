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
using AuditLogUtil;
using BLL.Framework;
using BLL;
public partial class Administration_PurgeAuditLog : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Administration.aspx");
    }
    protected void btnPurge_Click(object sender, EventArgs e)
    {
        EntValidationErrors vr = new EntValidationErrors();
        if (!ValidateBO.IsDate(txtDate.Text))
        {
            vr.Add("Invalid Date");
            ValidationErrorMessages1.ValidationErrors = vr;
        }
        else
        {
            //AuditLogBO al = new AuditLogBO();
            //long result = al.PurgAuditLog(Convert.ToDateTime(txtDate.Text));
            AuditLogDAC.PurgeAuditLogData("", Convert.ToDateTime(txtDate.Text));
            Response.Redirect("Administration.aspx");
            //vr.Add(result.ToString() + " audit records are removed.");
            //ValidationErrorMessages1.ValidationErrors = vr;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        IgnoreCapabilityCheck = true;
    }
    public override string MenuItemName()
    {
        return "Purge Audit Log";
    }
    public override string[] CapabilityNames()
    {
        throw new NotImplementedException();
    }
}
