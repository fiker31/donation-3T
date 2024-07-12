using BLL;
using BLL.Framework;
using DAL;
using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class Credit : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        //Session["LoginId"] = "eph";
        UserAccountBO currentUser = ((BasePage)Page).CurrentUser;
        //Set the top menu properties
        MenuTabs1.MenuItems = Globals.GetMenuItems(this.Cache);
        MenuTabs1.RootPath = BasePage.RootPath(Context);
        MenuTabs1.CurrentMenuItemName = ((BasePage)Page).MenuItemName();
        MenuTabs1.Roles = Globals.GetRoles(this.Cache);
        MenuTabs1.UserAccount = currentUser;
        //Set the side menu properties
        MenuTree1.MenuItems = Globals.GetMenuItems(this.Cache);
        MenuTree1.RootPath = BasePage.RootPath(Context);
        MenuTree1.CurrentMenuItemName = ((BasePage)Page).MenuItemName();
        MenuTree1.Roles = Globals.GetRoles(this.Cache);
        MenuTree1.UserAccount = currentUser;
        List<EntMenuItemBO>.Enumerator entMenuItemBOs = (List<EntMenuItemBO>.Enumerator)MenuTree1.MenuItems.GetEnumerator();
        lblCurrentUser.InnerText = currentUser.DisplayText;
        txtPosition.InnerText = currentUser.Position;
        //Page.User.Identity.Name;
        //lblbranchCode.Text = currentUser.BranchCode == null ? "" : currentUser.BranchCode.ToString();
        //forvariable.BranchCode = lblbranchCode.Text;
        //lblCurrentDateTime.Text = DateTime.Now.ToString();
        //Set the version
        lblVersion.Text = ConfigurationManager.AppSettings["version"].ToString();
        spnCurrentUser.InnerText = currentUser.DisplayText;

    }

    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        //List<string> keys = new List<string>();
        //IDictionaryEnumerator enumerator = Cache.GetEnumerator();

        //while (enumerator.MoveNext())
        //    keys.Add(enumerator.Key.ToString());

        //for (int i = 0; i < keys.Count; i++)
        //    Cache.Remove(keys[i]);

        Session["LoginId"] = null;
        Response.Redirect("../Login.aspx");

    }
}
