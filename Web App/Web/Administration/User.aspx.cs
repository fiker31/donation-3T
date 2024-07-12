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
public partial class Administration_User : BaseEditPage<UserAccountBO>
{
    private const string VS_USER = "User";
    private const string VS_MODE = "Entry Mode";
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.NewButton_Click += new CreditEditPage.ButtonClickedHandler(Master_NewButton_Click);
        Master.EditButton_Click += new CreditEditPage.ButtonClickedHandler(Master_EditButton_Click);
        Master.SaveButton_Click += new CreditEditPage.ButtonClickedHandler(Master_SaveButton_Click);
        Master.CancelButton_Click += new CreditEditPage.ButtonClickedHandler(Master_CancelButton_Click);
        Master.DeleteButton_Click += new CreditEditPage.ButtonClickedHandler(Master_DeleteButton_Click);
        Master.FindButton_Click += new CreditEditPage.ButtonClickedHandler(Master_FindButton_Click);
        if (!IsPostBack)
        {
            Session.Remove(GymConst.SS_ID);
            MakeFormReadOnly(CapabilityNames().FirstOrDefault(), this.Controls);
        }
    }
    void Master_NewButton_Click(object sender, EventArgs e)
    {
        ViewState[VS_MODE] = 'N';
        Master.EnableButtons(false, false, true, true, false, false);
        //Enable & Clear controls
        MakeFormEditable(CapabilityNames().FirstOrDefault(), this.Controls);
        ClearForm(this.Controls);
    }
    void Master_EditButton_Click(object sender, EventArgs e)
    {
        if (((UserAccountBO)ViewState[VS_USER]).Id > 0)
        {
            ViewState[VS_MODE] = 'U';
            Master.EnableButtons(false, false, true, true, false, false);
            //Enable Controls
            MakeFormEditable(CapabilityNames().FirstOrDefault(), this.Controls);
        }
        else
        {
            Master.ValidationErrors.Add("Record is not selected.");
        }
    }
    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        EntValidationErrors validationErrors = new EntValidationErrors();
        UserAccountBO ua;
        if ((char)ViewState[VS_MODE] == 'N')
        {
            ua = new UserAccountBO();
            ua.DBAction = BaseEO.DBActionEnum.Insert;
        }
        else
        {
            ua = (UserAccountBO)ViewState[VS_USER];
            ua.DBAction = BaseEO.DBActionEnum.Update;
        }
        LoadObjectFromScreen(ua);
        if (!ua.Save(ref validationErrors, Convert.ToString(Session["LoginId"])))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            ResetButtons();
            //Refresh the Viewstate with current record 
            ua.Load(ua.Id);
            LoadScreenFromObject(ua);
            Globals.LoadUsers(Page.Cache);
        }
    }
    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        LoadScreenFromObject((UserAccountBO)ViewState[VS_USER]);
        ResetButtons();
    }
    void Master_DeleteButton_Click(object sender, EventArgs e)
    {
        //if (((UserAccountBO)ViewState[VS_USER]).Id > 0)
        //{
        //    EntValidationErrors validationErrors = new EntValidationErrors();
        //    UserAccountBO ie = (UserAccountBO)ViewState[VS_USER];
        //    ua.DBAction = BaseEO.DBActionEnum.Delete;
        //    if (!ua.Delete(ref validationErrors, Convert.ToString(Session["LoginId"])))
        //    {
        //        Master.ValidationErrors = validationErrors;
        //    }
        //    else
        //    {
        //        //Clear Controls
        //        ViewState[VS_USER] = new UserAccountBO();
        //    }
        //}
        //else
        //{
        //    Master.ValidationErrors.Add("Record is not selected.");
        //}
    }
    void Master_FindButton_Click(object sender, EventArgs e)
    {
        GoToSearchPage();
    }
    #endregion Events
    #region Overrides
    protected override void LoadObjectFromScreen(UserAccountBO baseEO)
    {
        baseEO.UserAccountName = txtUserAName.Text;
        baseEO.FirstName = txtFirstName.Text;
        baseEO.LastName = txtFatherName.Text;
        baseEO.Position = txtPosition.Text;
        baseEO.Department = txtDepartment.Text;
        baseEO.Email = txtEmail.Text;
        baseEO.BranchCode = cbobranch.SelectedValue;
        //baseEO.UserPasswordLen = Convert.ToByte( StringHelpers.Decrypt(((UserAccountBO)ViewState[VS_USER]).UserPassword).Length);
        if (txtPWD.Text != "" && txtPWD.Text != "Unchanged")
        {
            if (ViewState[VS_MODE].ToString() == "N" || txtPWD.Text != StringHelpers.Decrypt(((UserAccountBO)ViewState[VS_USER]).UserPassword))
            {
                //baseEO.UserPasswordLen = Convert.ToByte( txtPWD.Text.Trim().Length);
                baseEO.UserPassword = StringHelpers.Encrypt(txtPWD.Text);
                baseEO.UserConfirmPassword = StringHelpers.Encrypt(txtConfirmPassword.Text);
                baseEO.PasswordChangeDate = DateTime.Now;
                baseEO.IsNewPassword = true;
            }
        }
        baseEO.IsActive = chkActive.Checked;
        baseEO.IsLocked = chkLocked.Checked;
    }
    protected override void LoadScreenFromObject(UserAccountBO baseEO)
    {

        txtUserAName.Text = baseEO.UserAccountName;
        txtFirstName.Text = baseEO.FirstName;
        txtFatherName.Text = baseEO.LastName;
        txtPosition.Text = baseEO.Position;
        txtDepartment.Text = baseEO.Department;
        txtEmail.Text = baseEO.Email;
        if (baseEO.UserPassword != null && baseEO.UserPassword != "Unchanged")
            txtPWD.Text = StringHelpers.Decrypt(baseEO.UserPassword);
        txtConfirmPassword.Text = txtPWD.Text;
        chkActive.Checked = baseEO.IsActive;
        chkLocked.Checked = baseEO.IsLocked;
        cbobranch.SelectedValue = baseEO.BranchCode;
        //Put the object in the view state so it can be attached back to the data context.
        ViewState[VS_USER] = baseEO;
    }
    protected override void LoadControls()
    {
        BranchBOList branch = new BranchBOList();
        branch.Load();
        cbobranch.DataSource = branch;
        cbobranch.DataValueField = "ID";
        cbobranch.DataTextField = "Name";
        cbobranch.DataBind();
        cbobranch.Items.Insert(0, new ListItem("--Select Branch Name--", "0"));
    }
    protected override void GoToSearchPage()
    {
        Session[GymConst.SS_ID] = ((UserAccountBO)ViewState[VS_USER]).Id.ToString();
        Response.Redirect("AdminSearchPage.aspx" + EncryptQueryString("page=User.aspx"));
    }
    protected override void ResetButtons()
    {
        Master.EnableButtons(true, true, false, false, true, true);
        MakeFormReadOnly(CapabilityNames().FirstOrDefault(), this.Controls);
    }
    public override string MenuItemName()
    {
        return "Users";
    }
    public override string[] CapabilityNames()
    {
        return new string[] { "Users" };
    }
    public override void CustomEditableLogic(string capabilityName)
    {
        if (ViewState[VS_MODE].ToString() == "U") txtUserAName.Enabled = false;
    }
    #endregion Overrides

}
