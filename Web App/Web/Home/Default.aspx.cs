using BLL;
using BLL.Framework;
using DAL;
using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get the Domestic Banking menu item from the cache
            EntMenuItemBO homeMenuItem = Globals.GetMenuItems(Cache).GetByMenuItemName("Home");
            //Create a node for each child element of the administrative menu.
            CreateChildNodes(tvMenuDescriptions.Nodes, homeMenuItem.ChildMenuItems);
        }

    }
    private void CreateChildNodes(TreeNodeCollection treeNodeCollection, EntMenuItemBOList childMenuItems)
    {
        foreach (EntMenuItemBO menuItem in childMenuItems)
        {
            TreeNode menuNode = new TreeNode(menuItem.MenuItemName + (menuItem.Description == null ? "" : ": " + menuItem.Description));
            menuNode.SelectAction = TreeNodeSelectAction.None;
            if (menuItem.ChildMenuItems.Count > 0)
            {
                CreateChildNodes(menuNode.ChildNodes, menuItem.ChildMenuItems);
            }
            treeNodeCollection.Add(menuNode);
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
}
