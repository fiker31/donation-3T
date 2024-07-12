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
public partial class Administration_SystemSecurity_ClearSystemSecurity : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SystemSecurityDAC.DbHasSecuritySetupTables())
        {
            lblConfirmMessage2.Text = "This option removes the system security setups from the current database(" + SystemSecurityDAC.SecuredDataBase + "). " + Environment.NewLine +
                    "I.e. all security setup tables will be dropped from the database." + Environment.NewLine + Environment.NewLine +
                    "As a result any security setup and password history  data will be destroyed and cannot be recoverd." + Environment.NewLine + Environment.NewLine +
                    "Do you realy want to remove system security tables?";
            if (!ClientScript.IsStartupScriptRegistered("clear"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "clear", "hasLogTableToClear();", true);
            }
        }
        else
        {
            lblConfirmMessage2.Text = " There are no system security tables to clear.";
            if (!ClientScript.IsStartupScriptRegistered("clear"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "clear", "noLogTableToClear();", true);
            }
        }
    }
    public override string MenuItemName()
    {
        return "Clear System Security Tables";
    }
    public override string[] CapabilityNames()
    {
        return (new string[] { "Clear System Security Tables" });
    }
    protected void btnClearSystemSecurity_Click(object sender, EventArgs e)
    {
        try
        {
            SystemSecurityDAC.ClearSecurityParameter();
            ValidationWarnings_Info1.ValidationErrors.Add("System Security setup Tables are Cleared Successfully.");
        }
        catch (Exception ex)
        {
            ValidationErrorMessages1.ValidationErrors.Add("System Security setup Tables are already cleared. (" + ex.Message + ")");
        }
    }
}
