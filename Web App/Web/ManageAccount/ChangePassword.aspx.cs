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
using SystemSecurityUtil;
public partial class ManageAccount_ChangePassword : BasePage
{
    const string VS_PAGE = "Source Page";
    const string VS_USER = "User";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Decrypt the query string
            //NameValueCollection queryString = DecryptQueryString(Request.QueryString.ToString());
            //if (queryString == null)
            //{
            //    ViewState[VS_PAGE] = null;
            //}
            //else
            //{
            //    //Get the source page
            //    ViewState[VS_PAGE] = queryString["page"];
            //    if (ViewState[VS_PAGE].ToString() == "Login.aspx")
            //    {
            //    }
            //}
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        EntValidationErrors validationErrors = new EntValidationErrors();
        UserAccountBO ua = new UserAccountBO();
        ua.Load(Session["LoginId"].ToString());
        SecurityParameter sp = SystemSecurityDAC.DbHasSecuritySetupTables() ?
        SystemSecurityDAC.GetSecurityParameterObject() : SystemSecurityDAC.GetEmpitySecurityParameterObject();
        PasswordStrengthUtil pwd = new PasswordStrengthUtil(
                                       sp.PasswordMinLength, sp.PasswordMustHaveDigit,
                                       sp.PasswordMustHaveLowerCase, sp.PasswordMustHaveUpperCase,
                                       sp.PasswordMustHaveSpecialChar);
        #region Validation
        if (StringHelpers.Decrypt(ua.UserPassword) != txtOldPassword.Text)
        {
            validationErrors.Add("Old Password is not Correct.");
        }
        else if (txtOldPassword.Text == txtPassword.Text)
        {
            validationErrors.Add("Old password and new password cannot be the same.");
        }
        else if (txtPassword.Text.CompareTo(txtConfirmPassword.Text) != 0)
        {
            validationErrors.Add("The new password and confirmation are not the same.");
        }
        else if (!pwd.IsStrong(txtPassword.Text))
        {
            foreach (string err in pwd.ValidationErrors)
                validationErrors.Add(err);
        }
        #endregion validation
        else
        {
            //ua.UserPassword = StringHelpers.Encrypt(txtPassword.Text);
            //ua.UserConfirmPassword = StringHelpers.Encrypt(txtConfirmPassword.Text);
            if (ua.ChangePassword(ref validationErrors, StringHelpers.Encrypt(txtPassword.Text),
                StringHelpers.Encrypt(txtConfirmPassword.Text), StringHelpers.Encrypt(txtOldPassword.Text), Session["LoginId"].ToString(), false))
            //ua.DBAction = BaseEO.DBActionEnum.Update;
            //ua.PasswordChangeDate = DateTime.Now;
            //ua.UserPasswordLen = Convert.ToByte(txtPassword.Text.Trim().Length);
            //ua.IsNewPassword = false;
            //if (!ua.Save(ref validationErrors, Convert.ToString(Session["LoginId"])))
            //{
            //    validationErrors.Add(
            //}
            //else
            {
                Response.Redirect("../Home/Default.aspx");
            }
        }
        if (validationErrors.Count != 0)
        {
            //validationErrors.Add("Old Password is not Correct.");
            ValidationErrorMessages1.ValidationErrors = validationErrors;
        }
    }
    public override string MenuItemName()
    {
        return "Home";
    }
    public override string[] CapabilityNames()
    {
        throw new NotImplementedException();
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        IgnoreCapabilityCheck = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
    }
}