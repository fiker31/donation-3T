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
using System.Collections.Generic;
using AuditLogUtil;
using BLL.Framework;
using BLL;
public partial class Administration_DbLogViewer : BaseInquiryPage
{
    private const string VS_ID = "Audit - Log";
    private const string VS_PK = "Primary Key";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState[VS_ID] = 0;
            ViewState[VS_PK] = "";
            LoadControls();
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsValidateInput())
        {
            AuditDataset.vwAuditLogDataTable dt = AuditLogDAC.GetAuditLogData(GetCriteria());
            gvQuery.DataSource = dt;
            gvQuery.DataBind();
        }
    }
    private AuditCriteria GetCriteria()
    {
        AuditCriteria ac = new AuditCriteria()
        {
            OperationId = (chkTable.Checked && ddlTable.Text != "" ? Convert.ToInt64(ViewState[VS_ID].ToString()) : 0), //(chkTable.Checked ? ((AuditTable)ViewState["table"]).TableId : 0),
            ColumnId = (chkColumn.Checked && ddlColumn.Text != "" ? Convert.ToInt64(ddlColumn.SelectedValue) : 0),
            UserName = (chkUserName.Checked ? ddlUserName.SelectedValue : ""),
            FromDate = Convert.ToDateTime(chkDateAfter.Checked ? (txtDateAfter.Text) : "01/01/1900"),
            ToDate = Convert.ToDateTime(chkDateBefore.Checked ? (txtDateBefore.Text) : "01/01/1900"),
            AuditInsert = cblEvent.Items[0].Selected,
            AuditUpdate = cblEvent.Items[1].Selected,
            AuditDelete = cblEvent.Items[2].Selected,
            RowKey = ""
        };
        if (txtRowKey.Text != "")
        {
            ac.RowKey = ViewState[VS_PK].ToString() + "=" + txtRowKey.Text;
        }
        return ac;
    }
    protected void LoadControls()
    {
        List<AuditTable> tables = new List<AuditTable>();
        tables = AuditLogDAC.GetAuditedTables();
        ddlTable.DataValueField = "TableName";
        ddlTable.DataTextField = "Description";
        ddlTable.DataSource = tables;
        ddlTable.DataBind();
        UserAccountBOList users = new UserAccountBOList();
        users.Load();
        ddlUserName.DataValueField = "UserAccountName";
        ddlUserName.DataTextField = "DisplayText";
        ddlUserName.DataSource = users;
        ddlUserName.DataBind();
        if (tables.Count != 0)
            LoadColumns();
    }
    protected void LoadColumns()
    {
        List<AuditColumn> columns = new List<AuditColumn>();
        AuditTable at = new AuditTable();
        at = AuditLogDAC.GetAuditedTable(ddlTable.SelectedValue);
        ViewState[VS_ID] = at.TableId;
        ViewState[VS_PK] = at.PrimaryKeyField;
        columns = at.AuditColumns;
        //columns =  new AuditLogBO().GetAuditedColumns(ddlTable.SelectedValue);
        ddlColumn.DataValueField = "ColumnId";
        ddlColumn.DataTextField = "ColumnName";
        ddlColumn.DataSource = columns;
        ddlColumn.DataBind();
    }
    protected void ddlTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadColumns();
    }
    protected void chkTable_CheckedChanged(object sender, EventArgs e)
    {
        chkColumn.Enabled = chkTable.Checked;
        ddlTable.Enabled = chkColumn.Enabled;
        txtRowKey.Enabled = chkTable.Enabled;
        txtRowKey.Text = "";
    }
    private bool IsValidateInput()
    {
        EntValidationErrors validationErrors = new EntValidationErrors();
        if (!(cblEvent.Items[0].Selected || cblEvent.Items[1].Selected || cblEvent.Items[2].Selected))
        {
            validationErrors.Add("Select at least one event type.");
        }
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
            SaveGridData(gvQuery, "auditlog_" + DateTime.Now.ToShortDateString() + ".csv");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Administration.aspx");
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
        return "View Audit Log";
    }
    public override string[] CapabilityNames()
    {
        throw new NotImplementedException();
    }
    protected void chkColumn_CheckedChanged(object sender, EventArgs e)
    {
        ddlColumn.Enabled = chkColumn.Checked;
    }
    protected void chkUserName_CheckedChanged(object sender, EventArgs e)
    {
        ddlUserName.Enabled = chkUserName.Checked;
    }
    protected void chkDateAfter_CheckedChanged(object sender, EventArgs e)
    {
        txtDateAfter.Enabled = chkDateAfter.Checked;
    }
    protected void chkDateBefore_CheckedChanged(object sender, EventArgs e)
    {
        txtDateBefore.Enabled = chkDateBefore.Checked;
    }
}
