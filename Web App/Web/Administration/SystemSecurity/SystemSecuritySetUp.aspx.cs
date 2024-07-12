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
public partial class Administration_SystemSecurity_SystemSecuritySetUp : BasePage
{
    #region Constants
    private const string VS_ROLE = "Role";
    private const string VS_MODE = "Entry Mode";
    #endregion Constants
    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MakeFormReadOnly(CapabilityNames().FirstOrDefault(), this.Controls);
            Session.Remove(GymConst.SS_ID);
            if (!SystemSecurityDAC.DbHasSecuritySetupTables())
            {
                PanelSystemSecurity.Visible = false;
                lnkBuildSystemSecurityTables.Visible = true;
                ValidationErrorMessages1.ValidationErrors.Add("There are no System Security tables  for the database (" + SystemSecurityDAC.SecuredDataBase + ").");
                ValidationWarnings_Info1.ValidationErrors.Add("To create System Security Tables use the link below.");
            }
            else
            {
                PanelSystemSecurity.Visible = true;
                lnkBuildSystemSecurityTables.Visible = false;
                LoadScreenFromObject();
            }
        }
    }
    #endregion Load
    #region Events
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState[VS_MODE] = 'U';
        EnableButtons(false, true, true);
        //Enable Controls
        MakeFormEditable(CapabilityNames().FirstOrDefault(), this.Controls);
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        EntValidationErrors validationErrors = new EntValidationErrors();
        if (!IsValidateInput()) return;
        SecurityParameter securityparameter = new SecurityParameter()
        {
            LoginIdMinLength = Convert.ToByte(txtMinLoginIdLength.Text),
            PasswordMinLength = Convert.ToByte(txtMinPassowrdLength.Text),
            PasswordMustHaveDigit = chkDigit.Checked,
            PasswordMustHaveLowerCase = chkLowerCase.Checked,
            PasswordMustHaveUpperCase = chkUpperCase.Checked,
            PasswordMustHaveSpecialChar = chkSpecial.Checked,
            NoOfUnsuccessfulAttempts = Convert.ToByte(txtUnsuccessfulAttempt.Text),
            PasswordInterval = Convert.ToByte(txtPasswordInterval.Text),
            PasswordHistory = Convert.ToByte(txtPasswordHistory.Text),
            WorkingHourFrom = txtWorkingHourFrom.Text,
            WorkingHourTo = txtWorkingHourTo.Text,
            WorkingHourToSat = txtWorkingHourToSat.Text,
            AllowWorkOnHoliday = chkAllowWorkOnHoliday.Checked,
            AllowWorkOnWeekend = chkAllowWorkOnWeekend.Checked,
            AllowWorkAfterWorkingHours = chkAllowWorkAfterWorkingHours.Checked,
            UpdateUserId = Convert.ToString(Session["LoginId"]),
            UpdateDate = DateTime.Now
        };
        if (SystemSecurityDAC.UpdateSecurityParameter(securityparameter))
        {
            //securityparameter = SystemSecurityDAC.GetAuditedTable(ddlAuditedTable.SelectedValue);
            LoadScreenFromObject();
            ResetButtons();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // LoadScreenFromObject();
        ResetButtons();
    }
    protected void lnkBuildSystemSecurityTables_Click(object sender, EventArgs e)
    {
        Response.Redirect("BuildSystemSecurityTables.aspx");
    }
    #endregion Events
    #region Overrides
    public override string[] CapabilityNames()
    {
        return (new string[] { "Set Up System Security" });
    }
    public override string MenuItemName()
    {
        return "Set Up System Security";
    }
    #endregion Overrides
    #region Other Methods
    protected void LoadObjectFromScreen()
    {
    }
    protected void LoadScreenFromObject()
    {
        SecurityParameter sp = SystemSecurityDAC.GetSecurityParameterObject();
        txtMinLoginIdLength.Text = sp.LoginIdMinLength.ToString();
        txtMinPassowrdLength.Text = sp.PasswordMinLength.ToString();
        chkDigit.Checked = sp.PasswordMustHaveDigit;
        chkLowerCase.Checked = sp.PasswordMustHaveLowerCase;
        chkUpperCase.Checked = sp.PasswordMustHaveUpperCase;
        chkSpecial.Checked = sp.PasswordMustHaveSpecialChar;
        txtUnsuccessfulAttempt.Text = sp.NoOfUnsuccessfulAttempts.ToString();
        txtPasswordInterval.Text = sp.PasswordInterval.ToString();
        txtPasswordHistory.Text = sp.PasswordHistory.ToString();
        txtWorkingHourFrom.Text = sp.WorkingHourFrom.ToString();
        txtWorkingHourTo.Text = sp.WorkingHourTo.ToString();
        txtWorkingHourToSat.Text = sp.WorkingHourToSat.ToString();
        chkAllowWorkOnHoliday.Checked = sp.AllowWorkOnHoliday;
        chkAllowWorkOnWeekend.Checked = sp.AllowWorkOnWeekend;
        chkAllowWorkAfterWorkingHours.Checked = sp.AllowWorkAfterWorkingHours;
    }
    protected void EnableButtons(bool editButton, bool applyButton, bool cancelButton)
    {
        btnEdit.Enabled = editButton;
        btnApply.Enabled = applyButton;
        btnCancel.Enabled = cancelButton;
    }
    protected void ResetButtons()
    {
        EnableButtons(true, false, false);
        MakeFormReadOnly(CapabilityNames().FirstOrDefault(), this.Controls);
    }
    #endregion Other Methods
    #region Validation
    private bool IsValidateInput()
    {
        EntValidationErrors validationErrors = new EntValidationErrors();
        if (txtMinLoginIdLength.Text == "")
        {
            validationErrors.Add("Minimum Login Id Length is required.");
        }
        else
        {
            if (!ValidateBO.IsInteger(txtMinLoginIdLength.Text))
            {
                validationErrors.Add("Minimum Login Id Length is not valid.");
            }
            else
            {
                if (Convert.ToInt32(txtMinLoginIdLength.Text) <= 0) validationErrors.Add("Minimum Login Id Length should be greater than 0.");
            }
        }
        if (txtMinPassowrdLength.Text == "")
        {
            validationErrors.Add("Minimum Password Length is required.");
        }
        else
        {
            if (!ValidateBO.IsInteger(txtMinPassowrdLength.Text))
            {
                validationErrors.Add("Minimum Password Length is not valid.");
            }
            else
            {
                if (Convert.ToInt32(txtMinPassowrdLength.Text) <= 0) validationErrors.Add("Minimum Password Length should be greater than 0.");
            }
        }
        if (txtPasswordHistory.Text == "")
        {
            validationErrors.Add("Pasword History is required.");
        }
        else
        {
            if (!ValidateBO.IsInteger(txtPasswordHistory.Text)) validationErrors.Add("Pasword History is not valid.");
        }
        if (txtUnsuccessfulAttempt.Text == "")
        {
            validationErrors.Add("Unsuccessful login attempt is required.");
        }
        else
        {
            if (!ValidateBO.IsInteger(txtUnsuccessfulAttempt.Text)) validationErrors.Add("Unsuccessful login attempt is not valid.");
        }
        if (txtPasswordInterval.Text == "")
        {
            validationErrors.Add("Password Interval is required.");
        }
        else
        {
            if (!ValidateBO.IsInteger(txtPasswordInterval.Text)) validationErrors.Add("Password Interval is not valid.");
        }
        if (txtWorkingHourFrom.Text == "")
        {
            validationErrors.Add("Working Hour Start is required.");
        }
        else
        {
            if (!ValidateBO.IsDate("01/01/2000 " + txtWorkingHourFrom.Text)) validationErrors.Add("Working Hour Start is not valid.");
        }
        if (txtWorkingHourTo.Text == "")
        {
            validationErrors.Add("Working Hour End is required.");
        }
        else
        {
            if (!ValidateBO.IsDate("01/01/2000 " + txtWorkingHourTo.Text)) validationErrors.Add("Working Hour End is not valid.");
        }
        if (txtWorkingHourToSat.Text == "")
        {
            validationErrors.Add("Working Hour End for Saturday is required.");
        }
        else
        {
            if (!ValidateBO.IsDate("01/01/2000 " + txtWorkingHourToSat.Text)) validationErrors.Add("Working Hour End for Saturday is not valid.");
        }
        if (validationErrors.Count() != 0)
        {
            ValidationErrorMessages1.ValidationErrors = validationErrors;
            return false;
        }
        return true;
    }
    #endregion Validation
}
