using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL.Framework;
using BLL;
public partial class BackUP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFileName.Text = ddlDatabase.SelectedValue + " " + DateTime.Now.ToString("yyyy-MM-dd hh-mm") + ".BAK";
            txtPath.Text = ConfigurationManager.AppSettings["BackupPath"].ToString();
        }

    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
    }
    protected void btnCreateBackup_Click(object sender, EventArgs e)
    {
        BackUpRestoreBO Backup = new BackUpRestoreBO();
        string filePath = txtPath.Text + txtFileName.Text;
        string sqlStatement = "BACKUP DATABASE " + ddlDatabase.SelectedValue + " TO DISK ='" + filePath + "' with init";
        if (Backup.BackUp(sqlStatement))
        {
            //Page.Controls.Add(new LiteralControl("<Script language = 'JavaScripts' window.alert('Backup complited Successfuly!')</script>"));
            ClientScript.RegisterStartupScript(this.GetType(), "Information", "alert('Backup completed successfully...');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Information", "alert('Backup is not completed successfully. Please try again or contact your system administrator...');", true);
        }
    }
    public override string MenuItemName()
    {
        return "Backup";
    }
    public override string[] CapabilityNames()
    {
        return new string[] { "Backup" };
    }
    protected void ddlDatabase_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFileName.Text = ddlDatabase.SelectedValue + DateTime.Now.ToString("yyyy-MM-dd hh-mm") + ".BAK";
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        IgnoreCapabilityCheck = true;
    }
}
