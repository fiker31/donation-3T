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
        if (SourcePage == "companyReg.aspx")
        {

            companyRegBOList result = new companyRegBOList();
            result.Load();
            dxgvSearch.KeyFieldName = "id";
            dxgvSearch.DataSource = result;
            dxgvSearch.DataBind();
            dxgvSearch.Columns["id"].Caption = "#";
            dxgvSearch.Columns["companyname"].MinWidth = 250;
            dxgvSearch.Columns["companydescription"].MinWidth = 250;
            dxgvSearch.Columns["location"].MinWidth = 200;
            dxgvSearch.Columns["phone"].MinWidth = 200;
            dxgvSearch.Columns["pobox"].MinWidth = 150;
            dxgvSearch.Columns["generalmanager"].MinWidth = 250;
            dxgvSearch.Columns["tillnumber"].MinWidth = 200;
            dxgvSearch.Columns["formationDate"].MinWidth = 150;
        }
        else if (SourcePage == "Donationmade.aspx")
        {
            DonationmadeBOList result = new DonationmadeBOList();
            result.Load();
            dxgvSearch.KeyFieldName = "id";
            dxgvSearch.DataSource = result;
            dxgvSearch.DataBind();
            dxgvSearch.Columns["id"].Caption = "#";
            dxgvSearch.Columns["DonationID"].MinWidth = 300;
            dxgvSearch.Columns["CustomerPhone"].MinWidth = 300;
            dxgvSearch.Columns["CustomerName"].MinWidth = 250;
            dxgvSearch.Columns["DonationAmount"].MinWidth = 250;
            dxgvSearch.Columns["DonationDate"].MinWidth = 250;

        }
        else if (SourcePage == "DonationRegList.aspx")
        {
           DonationReg_ListBOList result = new DonationReg_ListBOList();
            result.Load();
            dxgvSearch.KeyFieldName = "id";
            dxgvSearch.DataSource = result;
            dxgvSearch.DataBind();
            dxgvSearch.Columns["id"].Caption = "#";
            dxgvSearch.Columns["companyid"].MinWidth = 200;
            dxgvSearch.Columns["donationtitle"].MinWidth = 200;
            dxgvSearch.Columns["donationdescription"].MinWidth = 200;
            dxgvSearch.Columns["donationamountrequired"].MinWidth = 200;
            dxgvSearch.Columns["donationenddate"].MinWidth = 200;
            dxgvSearch.Columns["specialshortcode"].MinWidth = 200;
            dxgvSearch.Columns["donationrelatedlinks"].MinWidth = 200;
            dxgvSearch.Columns["sms"].MinWidth = 200;
            dxgvSearch.Columns["email"].MinWidth = 200;

        }
        else if (SourcePage == "Donator_p.aspx")
        {
            Donator_pBOList result = new Donator_pBOList();
            result.Load();
            dxgvSearch.KeyFieldName = "id";
            dxgvSearch.DataSource = result;
            dxgvSearch.DataBind();
            dxgvSearch.Columns["id"].Caption = "#";
            dxgvSearch.Columns["FullName"].MinWidth = 300;
            dxgvSearch.Columns["CustomerPhone"].MinWidth = 300;
            dxgvSearch.Columns["RecursiveDonations"].MinWidth = 300;


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
            case "companyReg.aspx":
                ViewState[VS_STRING_SRC_ID] = "N";
                ViewState[VS_SRC_ID] = Session[GymConst.SS_ID].ToString();
                ViewState[VS_MENU] = "Company Registration";
                break;
            case "Donationmade.aspx":
                ViewState[VS_STRING_SRC_ID] = "N";
                ViewState[VS_SRC_ID] = Session[GymConst.SS_ID].ToString();
                ViewState[VS_MENU] = "Donations Made";
                break;
            case "DonationRegList.aspx":
                ViewState[VS_STRING_SRC_ID] = "N";
                ViewState[VS_SRC_ID] = Session[GymConst.SS_ID].ToString();
                ViewState[VS_MENU] = "Donation Registration";
                break;
            case "Donator_p.aspx":
                ViewState[VS_STRING_SRC_ID] = "N";
                ViewState[VS_SRC_ID] = Session[GymConst.SS_ID].ToString();
                ViewState[VS_MENU] = "Donator";
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
        else if (ViewState[VS_MENU].ToString() == "Company Registration")
        {
            companyRegBO companyRegBO = (companyRegBO)dxgvSearch.GetRow(index);
            if (companyRegBO != null)
            {
                ViewState[VS_SEL_ID] = companyRegBO.id.ToString();
            }
        }
        else if (ViewState[VS_MENU].ToString() == "Donations Made")
        {
            DonationmadeBO DonationmadeBO = (DonationmadeBO)dxgvSearch.GetRow(index);
            if (DonationmadeBO != null)
            {
                ViewState[VS_SEL_ID] = DonationmadeBO.id.ToString();
            }
        }
        else if (ViewState[VS_MENU].ToString() == "Donation Registration")
        {
            DonationReg_ListBO DonationReg_ListBO = (DonationReg_ListBO)dxgvSearch.GetRow(index);
            if (DonationReg_ListBO != null)
            {
                ViewState[VS_SEL_ID] = DonationReg_ListBO.id.ToString();
            }
        }
        else if (ViewState[VS_MENU].ToString() == "Donator")
        {
            Donator_pBO Donator_pBO = (Donator_pBO)dxgvSearch.GetRow(index);
            if (Donator_pBO != null)
            {
                ViewState[VS_SEL_ID] = Donator_pBO.id.ToString();
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
