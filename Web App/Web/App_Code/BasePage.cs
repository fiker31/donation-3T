using BLL.Framework;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for BasePage
/// </summary>
public abstract class BasePage : System.Web.UI.Page
{
    #region Constructor
    public BasePage() { }
    #endregion Constructor
    #region Properities
    public UserAccountBO CurrentUser
    {
        get
        {
            try
            {
                return Globals.GetUsers(this.Cache).GetByUserAccountName(Session["LoginId"].ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    public bool IgnoreCapabilityCheck { get; set; }
    public bool ReadOnly { get; set; }
    public bool Edit { get; set; }
    public bool EditWithDelete { get; set; }
    #endregion Properties
    #region Abstract Methods
    public abstract string MenuItemName();
    /// <summary>
    /// The list of capabilities that a user needs to access this page.  A single page may have more than one
    /// capability associated with it.
    /// </summary>
    public abstract string[] CapabilityNames();
    #endregion Abstract Methods
    #region Public Methods
    /// <summary>
    /// Method returns the base application path.
    /// </summary>
    /// <param name="context">Context object</param>
    /// <returns>Returns the base application path.</returns>
    public static string RootPath(HttpContext context)
    {
        string urlSuffix = context.Request.Url.Authority + context.Request.ApplicationPath;
        return context.Request.Url.Scheme + @"://" + urlSuffix + "/";
    }
    public static NameValueCollection DecryptQueryString(string queryString)
    {
        return StringHelpers.DecryptQueryString(queryString);
    }
    public static string EncryptQueryString(NameValueCollection queryString)
    {
        return StringHelpers.EncryptQueryString(queryString);
    }
    /// <summary>
    /// You must pass in a string that uses the QueryStringHelper.DELIMITER as the delimiter.
    /// This will also append the "?" to the beginning of the query string.
    /// </summary>
    /// <param name="queryString"></param>
    /// <returns></returns>
    public static string EncryptQueryString(string queryString)
    {
        string s = StringHelpers.EncryptQueryString(queryString);
        return StringHelpers.EncryptQueryString(queryString);
    }
    // Report - Printing to PDF
    public string GetPrintButtonScript(Button btn)
    {
        StringBuilder printButtonScript = new StringBuilder();
        //Get the postback script.
        string postback = this.Page.ClientScript.GetPostBackEventReference(btn, "");
        //Change target to a new window.  Name the window the current date and time so multiple windows can
        //be opened.
        printButtonScript.Append("var today = new Date();");
        printButtonScript.Append("var newWindowName = today.getFullYear().toString() + today.getMonth().toString() + today.getDate().toString() + today.getHours().toString() + today.getHours().toString() + today.getMinutes().toString() + today.getSeconds().toString() + today.getMilliseconds().toString();");
        printButtonScript.Append("document.forms[0].target = newWindowName;");
        //TODO: Added root path after this was turned in.
        //Show the please wait screen.
        printButtonScript.Append("window.open('" + RootPath(base.Context) + "/Reports/PleaseWait.html', newWindowName, 'scrollbars=yes,status=no,resizable=yes');");
        //Add the postback script.
        printButtonScript.Append(postback + ";");
        //Reset target back to itself so other controls will post back to this form.
        printButtonScript.Append("document.forms[0].target='_self';");
        //Return false to stop page submitting.
        printButtonScript.Append("return false;" + Environment.NewLine);
        return printButtonScript.ToString();
    }
    #endregion Public Methods
    #region Overrides
    protected override void OnInit(EventArgs e)
    {
        if (Session["LoginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        base.OnInit(e);
        CheckCapabilities();
    }
    #endregion Overrides
    #region Virtual Methods
    public virtual void CheckCapabilities()
    {
        if (IgnoreCapabilityCheck == false)
        {
            foreach (string capabilityName in CapabilityNames())
            {
                //Check if the user has the capability to view this screen
                CapabilityBO capability = Globals.GetCapabilities(this.Cache).GetByName(capabilityName);
                if (capability == null)
                {
                    throw new Exception("Security is not enabled for this page. " + this.ToString());
                }
                else
                {
                    switch (CurrentUser.GetCapabilityAccess(capability.Id, Globals.GetRoles(this.Cache)))
                    {
                        case RoleCapabilityBO.CapabiiltyAccessFlagEnum.None:
                            NoAccessToPage(capabilityName);
                            break;
                        case RoleCapabilityBO.CapabiiltyAccessFlagEnum.ReadOnly:
                            MakeFormReadOnly(capabilityName, this.Controls);
                            ReadOnly = true;
                            break;
                        case RoleCapabilityBO.CapabiiltyAccessFlagEnum.Edit:
                            //Do not make the form read only.
                            Edit = true;
                            break;
                        case RoleCapabilityBO.CapabiiltyAccessFlagEnum.EditWithDelete:
                            //Do not make the form read only.
                            EditWithDelete = true;
                            break;
                        default:
                            throw new Exception("Unknown access for this screen. " + capability.CapabilityName);
                    }
                }
                capability = null;
            }
        }
    }
    protected virtual void NoAccessToPage(string capabilityName)
    {
        //The default implementation throws an error if the user came to a page and they do not have access
        //to the capability associated with that screen.
        //If a page has more than one capability you should override this method because a user could
        //have access to one section but not another so you do not want them to get an error
        throw new AccessViolationException("You do not have access to this screen.");
    }
    public virtual void CustomReadOnlyLogic(string capabilityName)
    {
        //Override this method in a page that has custom logic for non standard controls on the screen.
    }
    public virtual void CustomEditableLogic(string capabilityName)
    {
        //Override this method in a page that has custom logic for non standard controls on the screen.
    }
    /// <summary>
    /// The default implementation will make all controls disabled.
    /// If you have more than one capability associated with a page you should override this method
    /// with the special logic for each capability in the page.
    /// </summary>
    /// <param name="capabilityName"></param>
    /// <param name="controls"></param>
    public virtual void MakeFormReadOnly(string capabilityName, ControlCollection controls)
    {
        //ReadOnly = true;
        //MakeControlsReadOnly(controls);
        EnableDisableControls(controls, false);
        CustomReadOnlyLogic(capabilityName);
    }
    public virtual void MakeFormEditable(string capabilityName, ControlCollection controls)
    {
        //ReadOnly = true;
        EnableDisableControls(controls, true);
        CustomEditableLogic(capabilityName);
    }
    /// <summary>
    /// Clear the contents of the controls on the page. The New button and a successful Delete should both call this.
    /// </summary>
    /// 
    public virtual void ClearForm(ControlCollection controls)
    {
        //ReadOnly = true;
        ClearControls(controls);
        //CustomReadOnlyLogic(capabilityName);
    }
    #endregion Virtual Methods
    #region Private Methods
    //private void MakeControlsReadOnly(ControlCollection controls, bool enabled)
    //{                        
    //    foreach (Control c in controls)
    //    {
    //        if (c is TextBox)
    //        {
    //            ((TextBox)c).Enabled = false;
    //        }
    //        else if (c is RadioButton)
    //        {
    //            ((RadioButton)c).Enabled = false;
    //        }
    //        else if (c is DropDownList)
    //        {
    //            ((DropDownList)c).Enabled = false;
    //        }
    //        else if (c is CheckBox)
    //        {
    //            ((CheckBox)c).Enabled = false;
    //        }
    //        else if (c is RadioButtonList)
    //        {
    //            ((RadioButtonList)c).Enabled = false;
    //        }
    //        if (c.HasControls())
    //        {
    //            MakeControlsReadOnly(c.Controls);
    //        }
    //    }
    //}
    private void EnableDisableControls(ControlCollection controls, bool ForEnable)
    {
        foreach (Control c in controls)
        {
            if (c is TextBox)
            {
                ((TextBox)c).Enabled = ForEnable;
            }
            else if (c is RadioButton)
            {
                ((RadioButton)c).Enabled = ForEnable;
            }
            else if (c is DropDownList)
            {
                ((DropDownList)c).Enabled = ForEnable;
            }
            else if (c is CheckBox)
            {
                ((CheckBox)c).Enabled = ForEnable;
            }
            else if (c is RadioButtonList)
            {
                ((RadioButtonList)c).Enabled = ForEnable;
            }
            else if (c is ListBox)
            {
                ((ListBox)c).Enabled = ForEnable;
            }
            if (c.HasControls())
            {
                EnableDisableControls(c.Controls, ForEnable);
            }
        }
    }
    private void ClearControls(ControlCollection controls)
    {
        foreach (Control c in controls)
        {
            if (c is TextBox)
            {
                ((TextBox)c).Text = "";
            }
            else if (c is Label)
            {
                //((Label)c).Enabled = false;
            }
            else if (c is DropDownList)
            {
                ((DropDownList)(c)).SelectedValue = "0";
            }

            if (c.HasControls())
            {
                ClearControls(c.Controls);
            }
        }
    }
    #endregion Private Methods
}