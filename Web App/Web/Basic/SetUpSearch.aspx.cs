using DevExpress.Web;
using BLL;
using BLL.Framework;
using DAL;
using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
public partial class SetUp_SetUpSearch : BasePage
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
        if (SourcePage == "Category.aspx")
        {

            Ticket_CategoryBOList result = new Ticket_CategoryBOList();
            result.LoadByProTempletName("");
            dxgvSearch.KeyFieldName = "Id";
            dxgvSearch.DataSource = result;
            dxgvSearch.DataBind();
            dxgvSearch.Columns["Id"].Caption = "#";
            dxgvSearch.Columns["CategoryName"].MinWidth = 600;
        }


        dxgvSearch.Columns["DB Action"].Visible = false;
        dxgvSearch.Columns["DB Name"].Visible = false;


        dxgvSearch.Columns["Display Text"].Visible = false;
        dxgvSearch.Columns["InsertUserId"].Visible = false;
        dxgvSearch.Columns["UpdateUserId"].Visible = false;
        dxgvSearch.Columns["UpdateDate"].Visible = false;
        dxgvSearch.Columns["EntryDate"].Visible = false;
        dxgvSearch.Columns["Version"].Visible = false;

    }
    private void InitSearch()
    {
        ViewState[VS_SEL_ID] = "0";
        ViewState[VS_STRING_SRC_ID] = "N"; //Source id is not String Type
        switch (ViewState[VS_PAGE].ToString())
        {
            case "Category.aspx":
                ViewState[VS_STRING_SRC_ID] = "N";
                ViewState[VS_SRC_ID] = Session[GymConst.SS_ID].ToString();
                ViewState[VS_MENU] = "Category";
                break;

        }
    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {
        int index = dxgvSearch.FocusedRowIndex;
        if (ViewState[VS_MENU].ToString() == "Category")
        {
            Ticket_CategoryBO ticket_CategoryBO = (Ticket_CategoryBO)dxgvSearch.GetRow(index);
            if (ticket_CategoryBO != null)
            {
                ViewState[VS_SEL_ID] = ticket_CategoryBO.Id.ToString();
            }
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
