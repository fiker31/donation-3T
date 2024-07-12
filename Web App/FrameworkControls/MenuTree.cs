using BLL.Framework;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace FrameworkControls
{
    [ToolboxData("<{0}:MenuTree  runat=server></{0}:MenuTree>")]
    public class MenuTree : WebControl
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
            string html = null;
            //Check if the menus are there.  In design mode this is null so you don't want to display an error.
            if (MenuItems == null)
            {
                html = "<div>Tree Goes Here</div>";
            }
            writer.Write(html);
        }
        protected override void CreateChildControls()
        {
            TreeView menuControl = new TreeView();
            //menuControl.SelectedNodeStyle.CssClass = "btn btn-xs btn-secondary";
            menuControl.ID = "tvSideMenu";
            menuControl.NodeWrap = true;
            if (UserAccount != null)
            {
                //Find the top most menu item.  This is the tab at the top.
                EntMenuItemBO topMenuItem = MenuItems.GetTopMenuItem(CurrentMenuItemName);
                CreateChildMenu(menuControl.Nodes, topMenuItem.ChildMenuItems);
            }
            //Controls.Add(menuControl);

            foreach (TreeNode items in menuControl.Nodes)
            {
                if (items.Parent == null)
                {

                    //      <a href="#" class="nav-link active">
                    //                    <i class="nav-icon fas fa-tachometer-alt"></i>
                    //                    <p>
                    //                        Dashboard
                    //<i class="right fas fa-angle-left"></i>
                    //                    </p>
                    //                </a>

                    HtmlGenericControl parentMenu = ControlGenerator("", "li", "", "nav-item ", "", "", "", "");

                    HtmlGenericControl parentMenuLink = ControlGenerator("", "a", "", "nav-link", "", "", "", "");

                    HtmlGenericControl parentMenuLinkIcon = ControlGenerator("", "i", "", "nav-icon fa fa-bars", "", "", "", "");
                    switch (items.Text)
                    {
                        case "Database":
                            parentMenuLinkIcon.Attributes["class"] = "nav-icon fa fa-database";
                            break;
                        case "Security":
                            parentMenuLinkIcon.Attributes["class"] = "nav-icon fa fa-shield-alt";
                            break;
                        case "Ticket":
                            parentMenuLinkIcon.Attributes["class"] = "nav-icon fa fa-ticket-alt";
                            break;
                        default:
                            break;
                    }
                    HtmlGenericControl parentMenuLinkText = ControlGenerator("", "p", "", "", "", "", "", "");

                    HtmlGenericControl parentMenuLinkTextSpan = ControlGenerator("", "span", "", "", "", "", "", "");
                    HtmlGenericControl parentMenuLinkIconRight = ControlGenerator("", "i", "", "right fas fa-angle-left", "", "", "", "");

                    parentMenuLinkTextSpan.InnerText = items.Text;
                    parentMenuLinkTextSpan.Style.Add("padding-left", "10px;");


                    parentMenuLinkText.Controls.Add(parentMenuLinkTextSpan);
                    parentMenuLinkText.Controls.Add(parentMenuLinkIconRight);

                    parentMenuLink.Controls.Add(parentMenuLinkIcon);

                    parentMenuLink.Controls.Add(parentMenuLinkText);
                    parentMenu.Controls.Add(parentMenuLink);

                    HtmlGenericControl childMenusUL = ControlGenerator("", "ul", "", "nav nav-treeview", "", "", "", "");
                    HtmlGenericControl childMenu = new HtmlGenericControl(); ;

                    string s = items.Text;
                    foreach (TreeNode item in items.ChildNodes)
                    {
                        childMenu = ControlGenerator("", "li", "", "nav-item", "", "", "", "");
                        HtmlGenericControl childMenuLink = ControlGenerator("", "a", "", "nav-link", "", "", "", "");
                        childMenuLink.Attributes["href"] = item.NavigateUrl;
                        if (item.Selected == true)
                        {
                            parentMenuLink.Attributes["class"] = "nav-link active";
                            childMenuLink.Attributes["class"] = "nav-link active";

                        }

                        HtmlGenericControl childMenuLinkIcon = ControlGenerator("", "i", "", "fa fa-circle nav-icon", "", "", "", "");
                        switch (item.Text)
                        {
                            case "Organizer":
                                childMenuLinkIcon.Attributes["class"] = "nav-icon fa fa-user-lock";
                                break;
                            case "Basic":
                                childMenuLinkIcon.Attributes["class"] = "nav-icon fa fa-list-ul";
                                break;
                            case "Category":
                                childMenuLinkIcon.Attributes["class"] = "nav-icon fa fa-project-diagram";
                                break;
                            case "Users":
                                childMenuLinkIcon.Attributes["class"] = "nav-icon fa fa-user-edit";
                                break;
                            case "Roles":
                                childMenuLinkIcon.Attributes["class"] = "nav-icon fa fa-cog";
                                break;
                            case "Backup":
                                childMenuLinkIcon.Attributes["class"] = "nav-icon fa fa-download";
                                break;
                            case "Restore":
                                childMenuLinkIcon.Attributes["class"] = "nav-icon fa fa-undo-alt";
                                break;
                            default:
                                break;
                        }
                        HtmlGenericControl childMenuLinkText = ControlGenerator("", "p", "", "", "", "", "", "");
                        childMenuLinkText.InnerText = item.Text;


                        childMenuLink.Controls.Add(childMenuLinkIcon);
                        childMenuLink.Controls.Add(childMenuLinkText);
                        childMenu.Controls.Add(childMenuLink);


                        string ss = item.NavigateUrl;
                        childMenusUL.Controls.Add(childMenu);

                        parentMenu.Controls.Add(childMenusUL);
                    }
                    Controls.Add(parentMenu);


                }
            }
            base.CreateChildControls();
        }
        #endregion Overrides
        #region Private Methods
        private void CreateChildMenu(TreeNodeCollection nodes, EntMenuItemBOList menuItems)
        {
            foreach (EntMenuItemBO mi in menuItems)
            {
                //Check if the user has access to the menu or any children.
                if (mi.HasAccessToMenu(UserAccount, Roles))
                {
                    //Create an instance of the menu
                    TreeNode menuNode = new TreeNode(mi.MenuItemName, mi.Id.ToString(), "",
                            (string.IsNullOrEmpty(mi.Url) ? "" : RootPath + mi.Url), "");
                    if (string.IsNullOrEmpty(mi.Url))
                    {
                        menuNode.SelectAction = TreeNodeSelectAction.None;
                    }
                    //Check if this is the menu item that should be selected.
                    if (mi.MenuItemName == CurrentMenuItemName)
                    {
                        menuNode.Selected = true;
                    }
                    //Check if this has children.
                    if (mi.ChildMenuItems.Count > 0)
                    {
                        //Create items for the children.
                        CreateChildMenu(menuNode.ChildNodes, mi.ChildMenuItems);
                    }
                    nodes.Add(menuNode);
                }
            }
        }

        public HtmlGenericControl ControlGenerator(string Id, string ControlType, string Title, string ClassName, string InnerText, string Src, string Margin, string DataCardWidget)
        {
            HtmlGenericControl htmlGenericControl = new HtmlGenericControl(ControlType);
            htmlGenericControl.ID = Id;
            htmlGenericControl.Attributes["class"] = ClassName;
            htmlGenericControl.Attributes["title"] = Title;
            htmlGenericControl.InnerText = InnerText;
            htmlGenericControl.Attributes["src"] = Src;
            htmlGenericControl.Style.Add("margin", Margin);
            if (ControlType == "a")
            {
                htmlGenericControl.Attributes["href"] = "#";
            }
            htmlGenericControl.Attributes["runat"] = "server";
            return htmlGenericControl;
        }
        #endregion Private Methods
    }
}
