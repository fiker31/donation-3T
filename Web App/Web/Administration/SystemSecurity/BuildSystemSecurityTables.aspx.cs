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
using BLL.Framework;
using BLL;
using System.Collections.Generic;
using SystemSecurityUtil;
public partial class Administration_SystemSecurity_BuildSystemSecurityTables : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SystemSecurityDAC.DbHasSecuritySetupTables())
        {
            //AuditLogDAC.SetupAuditing();
            lblConfirmMessage1.Text = "Security Setup tables for the database have already been created" + Environment.NewLine +
            "Regenerating Security Setup Tables removes the Security Setup of the system from the current database(" + SystemSecurityDAC.SecuredDataBase + "). " + Environment.NewLine +
                    "I.e. all Security Setup tables will be regenerated." + Environment.NewLine + Environment.NewLine +
                    "As a result any Security Setup data will be destroyed and cannot be recoverd." + Environment.NewLine + Environment.NewLine +
                    "Do you realy want to regenerate database Security Setup tables ?";
            lblregenerate.Text = "Re-generate the Security Setup tables";
            //Page.RegisterClientScriptBlock ("xx", "document.getElementById(\"chkRegenerate\").checked = 0");
            if (!ClientScript.IsStartupScriptRegistered("alert"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "alert", "hasLogTable();", true);
            }
        }
        else
        {
            if (!ClientScript.IsStartupScriptRegistered("alert"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "alert", "noLogTable();", true);
            }
            //Load Confirm text for creating and clearing audit log setup
            lblConfirmMessage1.Text = "To perform System Security task security setup tables must be created <br/> in the database(" + SystemSecurityDAC.SecuredDataBase + ").<br/>" +
                "<br/><br/> Do You want to create the tables now?";
            lblregenerate.Text = "";
        }
    }
    public override string MenuItemName()
    {
        return "Build System Security Tables";
    }
    public override string[] CapabilityNames()
    {
        return (new string[] { "Build System Security Tables" });
    }
    protected void btnBuildSystemSecurityTables_Click(object sender, EventArgs e)
    {
        try
        {
            SystemSecurityDAC.SetupSecurityParameter();
            ValidationWarnings_Info1.ValidationErrors.Add("Security setup Tables are generated Successfully.");
        }
        catch (Exception ex)
        {
            ValidationErrorMessages1.ValidationErrors.Add(ex.Message);
        }
    }
    protected void chkRegenerate_CheckedChanged(object sender, EventArgs e)
    {
        //ButtonOK.Visible = chkRegenerate.Checked;
        //ModalPopupExtender1.Show();
    }
}
