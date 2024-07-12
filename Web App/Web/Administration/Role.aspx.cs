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
using System.Collections.Generic;
public partial class Administration_Role : BaseEditPage<RoleBO>
{
    private const string VS_ROLE = "Role";
    private const string VS_MODE = "Entry Mode";
    //private const string VS_CONTROL = "Control Collections";
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
        if (((RoleBO)ViewState[VS_ROLE]).Id > 0)
        {
            ViewState[VS_MODE] = 'U';
            Master.EnableButtons(false, false, true, true, false, false);
            //Enable Controls
            MakeFormEditable(CapabilityNames().FirstOrDefault(), this.Controls);
            lstSelectedUsers.Enabled = true;
            lstUnselectedUsers.Enabled = true;
        }
        else
        {
            Master.ValidationErrors.Add("Record is not selected.");
        }
    }
    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        EntValidationErrors validationErrors = new EntValidationErrors();
        RoleBO role;
        if ((char)ViewState[VS_MODE] == 'N')
        {
            role = new RoleBO();
            role.DBAction = BaseEO.DBActionEnum.Insert;
        }
        else
        {
            role = (RoleBO)ViewState[VS_ROLE];
            role.DBAction = BaseEO.DBActionEnum.Update;
        }
        LoadObjectFromScreen(role);
        if (!role.Save(ref validationErrors, Convert.ToString(Session["LoginId"])))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            ResetButtons();
            //Refresh the Viewstate with current record 
            role.Load(role.Id);
            LoadScreenFromObject(role);
            //Reload the globals
            Globals.LoadUsers(Page.Cache);
            Globals.LoadRoles(Page.Cache);
        }
    }
    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        LoadScreenFromObject((RoleBO)ViewState[VS_ROLE]);
        ResetButtons();
    }
    void Master_DeleteButton_Click(object sender, EventArgs e)
    {
        if (((RoleBO)ViewState[VS_ROLE]).Id > 0)
        {
            EntValidationErrors validationErrors = new EntValidationErrors();
            RoleBO role = (RoleBO)ViewState[VS_ROLE];
            role.DBAction = BaseEO.DBActionEnum.Delete;
            if (!role.Delete(ref validationErrors, Convert.ToString(Session["LoginId"])))
            {
                Master.ValidationErrors = validationErrors;
            }
            else
            {
                //Clear Controls
                ViewState[VS_ROLE] = new RoleBO();
            }
        }
        else
        {
            Master.ValidationErrors.Add("Record is not selected.");
        }
    }
    void Master_FindButton_Click(object sender, EventArgs e)
    {
        GoToSearchPage();
    }
    protected void btnMoveToSelected_Click(object sender, EventArgs e)
    {
        MoveItems(lstUnselectedUsers, lstSelectedUsers, false);
    }
    protected void btnMoveToUnselected_Click(object sender, EventArgs e)
    {
        MoveItems(lstSelectedUsers, lstUnselectedUsers, false);
    }
    protected void btnMoveAllToSelected_Click(object sender, EventArgs e)
    {
        MoveItems(lstUnselectedUsers, lstSelectedUsers, true);
    }
    protected void btnMoveAllToUnselected_Click(object sender, EventArgs e)
    {
        MoveItems(lstSelectedUsers, lstUnselectedUsers, true);
    }
    protected override void OnInit(EventArgs e)
    {
        //You need to build the table here so it retains state between postbacks.
        BuildCapabilityTable();
        base.OnInit(e);
    }
    protected override void LoadObjectFromScreen(RoleBO baseEO)
    {
        baseEO.RoleName = txtRoleName.Text;
        //Load the capabilities
        for (int row = 0; row < tblCapabilities.Rows.Count; row++)
        {
            TableRow tr = tblCapabilities.Rows[row];
            if (tr.Cells.Count > 1)
            {
                //The 2nd cell has the radio list
                RadioButtonList radioButtons = (RadioButtonList)tr.Cells[1].Controls[0];

                //The radio button's id contains the id of the capability.
                long capabilityId = Convert.ToInt64(radioButtons.ID);
                string value = radioButtons.SelectedValue;
                RoleCapabilityBO.CapabiiltyAccessFlagEnum accessFlag = (RoleCapabilityBO.CapabiiltyAccessFlagEnum)Enum.Parse(typeof(RoleCapabilityBO.CapabiiltyAccessFlagEnum), value);
                //Try to find an existing record for this capability
                RoleCapabilityBO capability = baseEO.RoleCapabilities.GetByCapabilityID(capabilityId);
                if (capability == null)
                {
                    //New record
                    RoleCapabilityBO roleCapability = new RoleCapabilityBO();
                    roleCapability.RoleId = baseEO.Id;
                    roleCapability.Capability.Id = capabilityId;
                    roleCapability.AccessFlag = accessFlag;
                    roleCapability.DBAction = BaseEO.DBActionEnum.Insert;
                    baseEO.RoleCapabilities.Add(roleCapability);
                }
                else
                {
                    //Update an existing record
                    capability.AccessFlag = accessFlag;
                    capability.DBAction = BaseEO.DBActionEnum.Update;
                }
            }
        }
        //Load the selected users                
        //Add any users that were not in the role before.
        foreach (ListItem li in lstSelectedUsers.Items)
        {
            //Check if they were already selected.
            if (baseEO.RoleUserAccounts.IsUserInRole(Convert.ToInt64(li.Value)) == false)
            {
                //If they weren't then add them.
                baseEO.RoleUserAccounts.Add(new RoleUserAccountBO { UserAccountId = Convert.ToInt64(li.Value), RoleId = baseEO.Id, DBAction = BaseEO.DBActionEnum.Insert });
            }
            else
            {
                //Mark them for deletion.
                RoleUserAccountBO user = baseEO.RoleUserAccounts.GetByUserAccountId(Convert.ToInt64(li.Value));
                user.DBAction = BaseEO.DBActionEnum.Update;
            }
        }
        //Remove any users that used to be selected but now are not.
        foreach (ListItem li in lstUnselectedUsers.Items)
        {
            //Check if they were in the role before
            if (baseEO.RoleUserAccounts.IsUserInRole(Convert.ToInt64(li.Value)))
            {
                //Mark them for deletion.
                RoleUserAccountBO user = baseEO.RoleUserAccounts.GetByUserAccountId(Convert.ToInt64(li.Value));
                user.DBAction = BaseEO.DBActionEnum.Delete;
            }
        }
    }
    protected override void LoadScreenFromObject(RoleBO baseEO)
    {
        RoleBO role = (RoleBO)baseEO;
        txtRoleName.Text = role.RoleName;
        //Select the capabilities        
        for (int row = 0; row < tblCapabilities.Rows.Count; row++)
        {
            TableRow tr = tblCapabilities.Rows[row];
            if (tr.Cells.Count > 1)
            {
                //The 2nd cell has the radio list
                RadioButtonList radioButtons = (RadioButtonList)tr.Cells[1].Controls[0];
                //Check if the role has this capability            
                RoleCapabilityBO capability = role.RoleCapabilities.GetByCapabilityID(Convert.ToInt64(radioButtons.ID));
                if (capability != null)
                {
                    //set the access
                    radioButtons.SelectedValue = capability.AccessFlag.ToString();
                }
                else
                {
                    //default to none.
                    radioButtons.SelectedIndex = 0;
                }
                capability = null;
            }
        }
        //Clear the list contents
        lstUnselectedUsers.Items.Clear();
        lstSelectedUsers.Items.Clear();
        //Select the users
        //Get all the users
        UserAccountBOList users = Globals.GetUsers(Page.Cache);
        foreach (UserAccountBO user in users)
        {
            if (role.RoleUserAccounts.IsUserInRole(user.Id))
            {
                lstSelectedUsers.Items.Add(new ListItem(user.DisplayText, user.Id.ToString()));
            }
            else
            {
                lstUnselectedUsers.Items.Add(new ListItem(user.DisplayText, user.Id.ToString()));
            }
        }
        ViewState[VS_ROLE] = role;
    }
    protected override void GoToSearchPage()
    {
        Session[GymConst.SS_ID] = ((RoleBO)ViewState[VS_ROLE]).Id.ToString();
        Response.Redirect("AdminSearchPage.aspx" + EncryptQueryString("page=Role.aspx"));
    }
    protected override void ResetButtons()
    {
        Master.EnableButtons(true, true, false, false, true, true);
        MakeFormReadOnly(CapabilityNames().FirstOrDefault(), this.Controls);
    }
    protected override void LoadControls()
    {
    }
    public override string[] CapabilityNames()
    {
        return (new string[] { "Roles" });
    }
    public override string MenuItemName()
    {
        return "Roles";
    }
    public override void CustomReadOnlyLogic(string capabilityName)
    {
        lstSelectedUsers.Enabled = false;
        lstUnselectedUsers.Enabled = false;
        btnMoveAllToSelected.Enabled = false;
        btnMoveAllToUnselected.Enabled = false;
        btnMoveToSelected.Enabled = false;
        btnMoveToUnselected.Enabled = false;
        //base.CustomReadOnlyLogic(capabilityName);
        //If this is read only then do not show the available choice for the users or the buttons to 
        //swap between list boxes
        //lstUnselectedUsers.Visible = false;
        //btnMoveAllToSelected.Visible = false;
        //btnMoveAllToUnselected.Visible = false;
        //btnMoveToSelected.Visible = false;
        //btnMoveToUnselected.Visible = false;
        //lblUsers.Visible = false;
        //lblUserHeader.Visible = false;
    }
    public override void CustomEditableLogic(string capabilityName)
    {
        lstSelectedUsers.Enabled = true;
        lstUnselectedUsers.Enabled = true;
        btnMoveAllToSelected.Enabled = true;
        btnMoveAllToUnselected.Enabled = true;
        btnMoveToSelected.Enabled = true;
        btnMoveToUnselected.Enabled = true;
    }
    #region Private Methods
    private void MoveItems(ListBox lstSource, ListBox lstDestination, bool moveAll)
    {
        for (int i = 0; i < lstSource.Items.Count; i++)
        {
            ListItem li = lstSource.Items[i];
            if ((moveAll == true) || (li.Selected == true))
            {
                //Add to destination
                li.Selected = false;
                lstDestination.Items.Add(li);
                lstSource.Items.RemoveAt(i);
                i--;
            }
        }
    }
    /// <summary>
    /// Build the capabilities grid in the OnInit event so that it keeps it state between
    /// postbacks.  This method just builds the grid, it does select the options for this role.
    /// That is handled in the LoadScreenFromObject method.
    /// </summary>
    private void BuildCapabilityTable()
    {
        //Get the capabilities
        CapabilityBOList capabilities = Globals.GetCapabilities(Page.Cache);
        //Get the menu items
        EntMenuItemBOList menuItems = Globals.GetMenuItems(Page.Cache);
        AddCapabilitiesForMenuItems(menuItems, capabilities, "");
    }
    private void AddCapabilitiesForMenuItems(EntMenuItemBOList menuItems, CapabilityBOList capabilities, string indentation)
    {
        //Loop around each menu item and create a row for each menu item and capability associated with the menu item
        foreach (EntMenuItemBO menuItem in menuItems)
        {
            //Get any capabilities with this item
            IEnumerable<CapabilityBO> capabilitiesForMenuItem = capabilities.GetByMenuItemId(menuItem.Id);
            if (capabilitiesForMenuItem.Count() == 0)
            {
                //Just add the menu item to the row
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();
                LiteralControl lc = new LiteralControl();
                lc.Text = indentation + menuItem.MenuItemName;
                tc.CssClass = "capabilityHeader";
                tc.Controls.Add(lc);
                tc.ColumnSpan = 3;
                tr.Cells.Add(tc);
                tblCapabilities.Rows.Add(tr);
            }
            else
            {
                //If there is only one capability associated with this menu item then just display the
                //menu item name and the radio buttons
                if (capabilitiesForMenuItem.Count() == 1)
                {
                    AddCapabilityToTable(capabilitiesForMenuItem.ElementAt(0), indentation + menuItem.MenuItemName);
                }
                else
                {
                    //Add a row for each capability
                    foreach (CapabilityBO capability in capabilitiesForMenuItem)
                    {
                        AddCapabilityToTable(capability, indentation + menuItem.MenuItemName + " (" + capability.CapabilityName + ")");
                    }
                }
            }
            if (menuItem.ChildMenuItems.Count > 0)
            {
                AddCapabilitiesForMenuItems(menuItem.ChildMenuItems, capabilities, indentation + "---");
            }
        }
    }
    private void AddCapabilityToTable(CapabilityBO capability, string text)
    {
        TableRow tr = new TableRow();
        //Name
        TableCell tc = new TableCell();
        LiteralControl lc = new LiteralControl();
        lc.Text = text;
        tc.Controls.Add(lc);
        tr.Cells.Add(tc);
        //access flag
        TableCell tc1 = new TableCell();
        RadioButtonList radioButtons = new RadioButtonList();
        radioButtons.ID = capability.Id.ToString();
        switch (capability.AccessType)
        {
            case CapabilityBO.AccessTypeEnum.ReadOnlyEditDelete:
                radioButtons.Items.Add(new ListItem("None", RoleCapabilityBO.CapabiiltyAccessFlagEnum.None.ToString()));
                radioButtons.Items.Add(new ListItem("Read Only", RoleCapabilityBO.CapabiiltyAccessFlagEnum.ReadOnly.ToString()));
                radioButtons.Items.Add(new ListItem("Edit", RoleCapabilityBO.CapabiiltyAccessFlagEnum.Edit.ToString()));
                radioButtons.Items.Add(new ListItem("Edit with Delete", RoleCapabilityBO.CapabiiltyAccessFlagEnum.EditWithDelete.ToString()));
                radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                radioButtons.RepeatLayout = RepeatLayout.Table;
                break;
            case CapabilityBO.AccessTypeEnum.ReadOnlyEdit:
                radioButtons.Items.Add(new ListItem("None", RoleCapabilityBO.CapabiiltyAccessFlagEnum.None.ToString()));
                radioButtons.Items.Add(new ListItem("Read Only", RoleCapabilityBO.CapabiiltyAccessFlagEnum.ReadOnly.ToString()));
                radioButtons.Items.Add(new ListItem("Edit", RoleCapabilityBO.CapabiiltyAccessFlagEnum.Edit.ToString()));
                radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                radioButtons.RepeatLayout = RepeatLayout.Table;
                break;
            case CapabilityBO.AccessTypeEnum.ReadOnly:
                radioButtons.Items.Add(new ListItem("None", RoleCapabilityBO.CapabiiltyAccessFlagEnum.None.ToString()));
                radioButtons.Items.Add(new ListItem("Read Only", RoleCapabilityBO.CapabiiltyAccessFlagEnum.ReadOnly.ToString()));
                radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                radioButtons.RepeatLayout = RepeatLayout.Table;
                break;
            case CapabilityBO.AccessTypeEnum.Edit:
                radioButtons.Items.Add(new ListItem("None", RoleCapabilityBO.CapabiiltyAccessFlagEnum.None.ToString()));
                radioButtons.Items.Add(new ListItem("Edit", RoleCapabilityBO.CapabiiltyAccessFlagEnum.Edit.ToString()));
                radioButtons.RepeatDirection = RepeatDirection.Horizontal;
                radioButtons.RepeatLayout = RepeatLayout.Table;
                break;
        }

        tc1.Controls.Add(radioButtons);
        tc1.Style.Add("padding-left", "20px;");
        tr.Cells.Add(tc1);
        tblCapabilities.Rows.Add(tr);
    }
    #endregion Private Methods    
}
