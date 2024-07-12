using BLL.Framework;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace FrameworkControls
{
    [ToolboxData("<{0}:MenuTabs runat=server></{0}:MenuTabs>")]
    public class MenuTabs : WebControl
    {
        #region Properties
        [Browsable(false)]
        public EntMenuItemBOList MenuItems { get; set; }
        [Browsable(false)]
        public string CurrentMenuItemName { get; set; }
        [Browsable(true)]
        [DefaultValue("Enter Application Root Path")]
        [Description("Enter the root path for your application.  This is used to determine the path for all items in the menu.")]
        public string RootPath { get; set; }
        [Browsable(false)]
        public UserAccountBO UserAccount { get; set; }
        [Browsable(false)]
        public RoleBOList Roles { get; set; }
        #endregion Properties
        #region Overrides
        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
            string html;
            //Check if the menus are there.  In design mode this is null so you don't want to display an error.
            if (MenuItems != null)
            {
                //Get the parent menu item for the current menu item.  The parent will be the one with a null ParentMenuItemId
                EntMenuItemBO topMenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);
                html = "<li class=\"nav - item d - none d - sm - inline - block\">";
                //Loop around the top level items
                foreach (EntMenuItemBO mi in MenuItems)
                {
                    //Only show the tabs for the side menu item that the user has access to.                                
                    if (mi.HasAccessToMenu(UserAccount, Roles))
                    {
                        //Check if this is the selected menu tab.                        
                        if (mi.MenuItemName == topMenuItem.MenuItemName)
                        {
                            html += GetActiveTab(mi);
                        }
                        else
                        {
                            html += GetInactiveTab(mi);
                        }
                    }
                }
                html += "</li>";
            }
            else
            {
                html = "<div>Top Menu Goes Here</div>";
            }
            writer.Write(html);
        }
        #endregion Overrides
        #region Private Methods
        private string GetActiveTab(EntMenuItemBO subMenu)
        {
            //return "  <a Class=\"nav-link activeMenuItem\" href=\"" + RootPath + subMenu.Url + "\"><b><i class=\"fa fa-home\"></i> " + subMenu.MenuItemName + "</b></a>";
            return "";
        }
        private string GetInactiveTab(EntMenuItemBO subMenu)
        {
            //return "  <a Class=\"nav-link\" href=\"" + RootPath + subMenu.Url + "\"><b><i class=\"fa fa-home\"></i> " + subMenu.MenuItemName + "</b></a>";
            return "";

        }
        #endregion Private Methods
    }
}
