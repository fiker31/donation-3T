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
using FrameworkControls;
using SystemSecurityUtil;
public partial class Login : System.Web.UI.Page
{
    SecurityParameter sp = SystemSecurityDAC.DbHasSecuritySetupTables() ?
        SystemSecurityDAC.GetSecurityParameterObject() : SystemSecurityDAC.GetEmpitySecurityParameterObject();
    protected void Page_Load(object sender, EventArgs e)

    {
        //Set Application State values
        SestApplicationState();
        //SecurityParmBO.Load();
        lblVersion.Text = ConfigurationManager.AppSettings["version"].ToString();

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        EntValidationErrors validationErrors = new EntValidationErrors();

        //try
        //{
        //    AutenticateUser(txtLogin.Text, txtPassword.Text);
        //    if (chkFullLoad.Checked) MainLoad();
        //    //Session["LoginId"] = txtLogin.Text;
        //    FormsAuthentication.SetAuthCookie(txtLogin.Text, false);
        //}
        //catch (Exception ex)
        //{
        //    validationErrors.Add(ex.Message);
        //    ValidationErrorMessages1.ValidationErrors = validationErrors;

        //}

        UserAccountBO ua = new UserAccountBO();
        //Check If Login time is allowed at this time
        //Get and Validate working time
        WorkingTime worktime = new WorkingTime(
                                    sp.WorkingHourFrom, sp.WorkingHourTo,
                                    sp.WorkingHourToSat, sp.AllowWorkOnHoliday,
                                    sp.AllowWorkOnWeekend, sp.AllowWorkAfterWorkingHours);
        if (!worktime.IsLogInAllowed())
        {
            foreach (string err in worktime.ValidationErrors)
                validationErrors.Add(err);
            ValidationErrorMessages1.ValidationErrors = validationErrors;
            return;
        }
        if (!ua.Load(txtLogin.Text))
        {
            validationErrors.Add("Invalid User Account");
        }
        else
        {
            if (StringHelpers.Decrypt(ua.UserPassword) != txtPassword.Text)
            {
                //If allowed unsuccessful password attempts limited
                //then increment the password attempts lock user if the limit exceeds.
                if (sp.NoOfUnsuccessfulAttempts > 0 && !ua.IsLocked)
                {
                    ua.IncrementPasswordAttempt(ref validationErrors);
                }
                if (ua.IsLocked)
                {
                    validationErrors.Add("Your account is locked! Contact your system administrator.");
                }
                else
                {
                    validationErrors.Add("Invalid Password");
                }
            }
            else if (!ua.IsActive)
                validationErrors.Add("Your account is not active! Contact your system administrator.");
            else if (ua.IsLocked)
                validationErrors.Add("Your account is locked! Contact your system administrator.");
            //else if (IsAlreadyLogged())
            //    validationErrors.Add("User Already logged.");
        }
        if (validationErrors.Count() != 0)
            ValidationErrorMessages1.ValidationErrors = validationErrors;
        else
        {
            if (sp.NoOfUnsuccessfulAttempts > 0 && ua.PasswordAttempt != 0)
            {
                ua.RessetPasswordAttempt(ref validationErrors);
            }
            if (chkFullLoad.Checked) MainLoad();
            //Session["LoginId"] = txtLogin.Text;
            FormsAuthentication.SetAuthCookie(txtLogin.Text, false);
            if (ua.IsNewPassword || ua.IsPasswordExpired())
            {
                //lblMessage.Text = (ua.IsNewPassword ? "Your password is new/resetted by the administrator." :
                //                                       "Your password is expired.");
                Panel1.Visible = false;
                panaelChangePassword.Visible = true;
                txtNewPassword.Focus();
            }
            else
            {
                //TimeSpan sessTimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
                //HttpContext.Current.Cache.Insert(txtLogin.Text, txtLogin.Text, null, DateTime.MaxValue, sessTimeOut,
                //    System.Web.Caching.CacheItemPriority.NotRemovable, null);

                Session["SID"] = ua.UpdateUserLogInHistory(GetUserHostName());
                Session["LoginId"] = txtLogin.Text;
                //Taken From Birtukan
                //Added by Mesfin
                Session["UserId"] = ua.Id;
                Response.Redirect("Home/Default.aspx");
            }
        }
    }
    private void SestApplicationState()
    {
        //If Business Period is not saved to the application sate then save it
        if (Application["Period"] == null)
        {
            BusPeriodBO bp = new BusPeriodBO();
            if (bp.LoadCurrentPeriod())
            {
                Application["Period"] = bp;
            }
            else
            {
            }
        }
    }
    private void MainLoad()
    {
        Globals.LoadMenuItems(this.Cache);
        Globals.LoadRoles(this.Cache);
        Globals.LoadCapabilities(this.Cache);
        Globals.LoadUsers(this.Cache);
        //BusinessPeriodBO bp = new BusinessPeriodBO();
        //bp.LoadCurrentPeriod();
        //Application["Period"] = bp;
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        EntValidationErrors validationErrors = new EntValidationErrors();
        if (txtNewPassword.Text == "")
        {
            validationErrors.Add("New Password can not be empty.");
            ValidationErrorMessages2.ValidationErrors = validationErrors;
            return;
        }
        else if (txtNewPassword.Text.CompareTo(txtConfirmPassword.Text) != 0)
        {
            validationErrors.Add("Password and Confirm Passwords are not the same.");
            ValidationErrorMessages2.ValidationErrors = validationErrors;
            return;
        }
        else
        {
            //Get and Validate Password Strenth
            PasswordStrengthUtil pwd = new PasswordStrengthUtil(
                                         sp.PasswordMinLength, sp.PasswordMustHaveDigit,
                                         sp.PasswordMustHaveLowerCase, sp.PasswordMustHaveUpperCase,
                                         sp.PasswordMustHaveSpecialChar);
            if (!pwd.IsStrong(txtNewPassword.Text))
            {
                foreach (string err in pwd.ValidationErrors)
                    validationErrors.Add(err);
                ValidationErrorMessages2.ValidationErrors = validationErrors;
                return;
            }
        }
        UserAccountBO ua = new UserAccountBO();
        ua.Load(txtLogin.Text);
        //t.ToByte(txtNewPassword.Text.Trim().Length);
        if (!ua.ChangePassword(ref validationErrors, StringHelpers.Encrypt(txtNewPassword.Text),
                StringHelpers.Encrypt(txtConfirmPassword.Text), ua.UserPassword, txtLogin.Text, false))
        {
            ValidationErrorMessages2.ValidationErrors = validationErrors;
        }
        else
        {
            //Session["SID"] = ua.UpdateUserLogInHistory(GetUserHostName());
            Session["LoginId"] = txtLogin.Text;
            Response.Redirect("Home/Default.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["LoginId"] = null;
        Panel1.Visible = true;
        panaelChangePassword.Visible = false;
    }
    private string GetUserHostName()
    {
        return "";
        //System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(Request.UserHostName);
        //return host.HostName;
    }
    //private bool IsAlreadyLogged()
    //{
    //    string sKey = txtLogin.Text;
    //    string sUser = Convert.ToString(Cache[sKey]);
    //    return (!string.IsNullOrEmpty(sUser));
    //}

    [Obsolete]
    private void AutenticateUser(string userName, string password)
    {
        // Path to you LDAP directory server.
        // Contact your network administrator to obtain a valid path.

        string adPath = "LDAP://" + ConfigurationSettings.AppSettings["DefaultActiveDirectoryServer"];
        ActiveDirectoryValidator adAuth = new ActiveDirectoryValidator(adPath);
        if (true == adAuth.IsAuthenticated(userName, password))
        {
            // Create the authetication ticket
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(60), false, "");
            // Now encrypt the ticket.
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            // Create a cookie and add the encrypted ticket to the
            // cookie as data.
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            // Add the cookie to the outgoing cookies collection.
            HttpContext.Current.Response.Cookies.Add(authCookie);
            // Redirect the user to the originally requested page
            //HttpContext.Current.Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, false));
            HttpContext.Current.Response.Redirect("Home/Default.aspx");
        }

    }

}

