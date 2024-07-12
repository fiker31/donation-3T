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
using System.Collections.Specialized;
using BLL;
using BLL.Framework;
using DevExpress.Web;

public partial class Administration_AdminSearchPage : BasePage
{
    const string VS_SEL_ID = "Selected Id Value";
    const string VS_MENU = "Menu Name";
    const string VS_STRING_SRC_ID = "Is Source Id string"; //Y or N
    const string VS_SRC_ID = "Source Id Value"; //obtained from session(C_OBJECT)
    const string VS_PAGE = "Source Page";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Decrypt the query string 
            NameValueCollection queryString = DecryptQueryString(Request.QueryString.ToString());
            if (queryString == null)
            {
                ViewState[VS_PAGE] = null;
            }
            else
            {
                //Get the source page
                ViewState[VS_PAGE] = queryString["page"];
            }
        }
    }
    private void GoToEditPage()
    {
        Response.Redirect(ViewState[VS_PAGE].ToString() + EncryptQueryString("Id=" + GetReturnId()));
    }
    public override string MenuItemName()
    {
        return ViewState[VS_MENU].ToString();
    }
    public string GetReturnId()
    {
        if (ViewState[VS_SEL_ID].ToString() != "0")
        {
            return (ViewState[VS_STRING_SRC_ID].ToString() == "Y" ? "s" : "") + ViewState[VS_SEL_ID].ToString();
        }
        else
        {
            return (ViewState[VS_STRING_SRC_ID].ToString() == "Y" ? "s" : "") + ViewState[VS_SRC_ID].ToString();
        }
    }
    void Master_LoadButton_Click(object sender, EventArgs e)
    {
        GoToEditPage();
    }
    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        ViewState[VS_SEL_ID] = "0";
        GoToEditPage();
    }
    public override string[] CapabilityNames()
    {
        throw new NotImplementedException();
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        IgnoreCapabilityCheck = true;
        dxgvDataBind();
    }
    public void dxgvDataBind()
    {

        //Decrypt the query string
        NameValueCollection queryString = DecryptQueryString(Request.QueryString.ToString());
        if (queryString == null)
        {
            ViewState[VS_PAGE] = null;
        }
        else
        {
            //Get the source page
            ViewState[VS_PAGE] = queryString["page"];
            InitSearch();
        }

        //string SourcePage = ViewState[VS_PAGE].ToString();
        string SourcePage = ViewState[VS_PAGE].ToString();

        if (SourcePage == "Role.aspx")
        {
            RoleBOList result = new RoleBOList();
            result.LoadByRoleName("");
            dxgvSearch.KeyFieldName = "Id";
            dxgvSearch.DataSource = result;
            dxgvSearch.DataBind();
            dxgvSearch.Columns[0].Visible = false;
            dxgvSearch.Columns.Add(new GridViewCommandColumn() { ShowSelectCheckbox = true, VisibleIndex = 0 });
            dxgvSearch.Columns[1].MinWidth = 300;


        }
        if (SourcePage == "User.aspx")
        {
            UserAccountBOList result = new UserAccountBOList();
            result.LoadWithRoles();
            dxgvSearch.KeyFieldName = "Id";
            dxgvSearch.DataSource = result;
            dxgvSearch.DataBind();

            UserAccountBO userAccountBO = new UserAccountBO();
            //userAccountBO.Email

            dxgvSearch.Columns["UserAccountName"].Caption = "Username";
            dxgvSearch.Columns["UserFullName"].Caption = "Full Name";


            dxgvSearch.Columns["Email"].MinWidth = 300;
            dxgvSearch.Columns["Department"].MinWidth = 300;
            dxgvSearch.Columns["Position"].MinWidth = 300;
            dxgvSearch.Columns["LastName"].MinWidth = 150;
            dxgvSearch.Columns["FirstName"].MinWidth = 150;
            dxgvSearch.Columns["UserAccountName"].MinWidth = 150;
            dxgvSearch.Columns["UserFullName"].MinWidth = 200;



            dxgvSearch.Columns["BranchCode"].Visible = false;
            dxgvSearch.Columns["PasswordAttempt"].Visible = false;
            dxgvSearch.Columns["IsLocked"].Visible = false;
            dxgvSearch.Columns["PasswordChangeDate"].Visible = false;
            dxgvSearch.Columns["IsNewPassword"].Visible = false;
            dxgvSearch.Columns["UserPassword"].Visible = false;

            dxgvSearch.Columns[0].Visible = false;
            dxgvSearch.Columns.Add(new GridViewCommandColumn() { ShowSelectCheckbox = true, VisibleIndex = 0 });

        }
        dxgvSearch.Columns["InsertUserId"].Visible = false;
        dxgvSearch.Columns["UpdateUserId"].Visible = false;
        dxgvSearch.Columns["UpdateDate"].Visible = false;
        dxgvSearch.Columns["EntryDate"].Visible = false;
        dxgvSearch.Columns["Version"].Visible = false;

        dxgvSearch.Columns["DB Action"].Visible = false;
        dxgvSearch.Columns["DB Name"].Visible = false;
        dxgvSearch.Columns["Display Text"].Visible = false;



    }
    private void InitSearch()
    {
        ViewState[VS_SEL_ID] = "0";
        ViewState[VS_STRING_SRC_ID] = "N"; //Source id is not String Type
        switch (ViewState[VS_PAGE].ToString())
        {

            case "Role.aspx":
                ViewState[VS_STRING_SRC_ID] = "N";
                ViewState[VS_SRC_ID] = Session[GymConst.SS_ID].ToString();
                ViewState[VS_MENU] = "Roles";
                break;

            case "User.aspx":
                ViewState[VS_STRING_SRC_ID] = "N";
                ViewState[VS_SRC_ID] = Session[GymConst.SS_ID].ToString();
                ViewState[VS_MENU] = "Users";
                break;
        }
    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {

        int index = dxgvSearch.FocusedRowIndex;
        if (ViewState[VS_MENU].ToString() == "Roles")
        {
            RoleBO loanMemberBO = (RoleBO)dxgvSearch.GetRow(index);
            ViewState[VS_SEL_ID] = loanMemberBO.Id.ToString();
        }
        if (ViewState[VS_MENU].ToString() == "Users")
        {
            UserAccountBO collateral = (UserAccountBO)dxgvSearch.GetRow(index);
            ViewState[VS_SEL_ID] = collateral.Id.ToString();
        }
        GoToEditPage();

    }
    //protected void dxgvSearch_FocusedRowChanged(object sender, EventArgs e)
    //{
    //    string s = "";
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState[VS_SEL_ID] = "0";
        GoToEditPage();
    }
}
