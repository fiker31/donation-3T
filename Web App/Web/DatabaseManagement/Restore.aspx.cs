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
using System.IO;
using BLL.Framework;
public partial class DatabaseManagement_Restore : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DirectoryInfo diObj = new DirectoryInfo(ConfigurationManager.AppSettings["BackupPath"].ToString());
            //inserts all the files found in the default backup stroing sub-directory to the list box.
            FileInfo[] files = diObj.GetFiles(ddlDatabase.SelectedValue + "*");
            foreach (FileInfo file in files)
            {
                lstBackupFiles.Items.Add(file.ToString());
            }
        }
    }
    public override string MenuItemName()
    {
        return "Restore";
    }
    public override string[] CapabilityNames()
    {
        return new string[] { "Restore" };
    }
    protected void lstBackupFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSelectedFile.Text = lstBackupFiles.SelectedItem.Text;
    }
    protected void btnRestore_Click(object sender, EventArgs e)
    {
        EntValidationErrors validationErrors = new EntValidationErrors();
        if (lstBackupFiles.SelectedIndex == -1)
        {
            validationErrors.Add("File Not Selected");
            this.ValidationErrorMessages1.ValidationErrors = validationErrors;
            return;
        }
        BackUpRestoreBO Restore = new BackUpRestoreBO();
        string sqlStatement = "USE MASTER ALTER DATABASE " + ddlDatabase.SelectedValue + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE RESTORE DATABASE " +
                              ddlDatabase.SelectedValue + " FROM DISK = '" + ConfigurationManager.AppSettings["BackupPath"].ToString() +
                              txtSelectedFile.Text + "' WITH REPLACE";
        if (Restore.BackUp(sqlStatement))
        {
            //Page.Controls.Add(new LiteralControl("<Script language = 'JavaScripts' window.alert('Backup complited Successfuly!')</script>"));
            ClientScript.RegisterStartupScript(this.GetType(), "Information", "alert('Restore completed successfully...');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Information", "alert('Restore is not completed successfully. Please try again or contact your system administrator...');", true);
        }
    }
    protected void ddlDatabase_SelectedIndexChanged(object sender, EventArgs e)
    {
        DirectoryInfo diObj = new DirectoryInfo(ConfigurationManager.AppSettings["BackupPath"].ToString());
        FileInfo[] files = diObj.GetFiles(ddlDatabase.SelectedValue + "*");
        lstBackupFiles.Items.Clear();
        foreach (FileInfo file in files)
        {
            lstBackupFiles.Items.Add(file.ToString());
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        IgnoreCapabilityCheck = true;
    }
    protected void btnRestore_Click1(object sender, EventArgs e)
    {
    }
}
