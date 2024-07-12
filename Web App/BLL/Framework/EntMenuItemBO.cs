using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Framework;
namespace BLL.Framework
{
    #region ENTMenuItemBO
    [Serializable()]
    public class EntMenuItemBO
    {
        #region Constructor
        public EntMenuItemBO()
        {
            ChildMenuItems = new EntMenuItemBOList();
        }
        #endregion Constructor
        #region Properties
        public long Id { get; set; }
        public string MenuItemName { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Nullable<long> ParentMenuItemId { get; set; }
        public short DisplaySequence { get; set; }
        public bool IsAlwaysEnabled { get; set; }
        public EntMenuItemBOList ChildMenuItems { get; private set; }
        #endregion Properties
        #region methods
        internal void MapEntityToProperties(EntMenuItem menuItem)
        {
            Id = menuItem.MenuItemId;
            MenuItemName = menuItem.MenuItemName;
            Description = menuItem.Description;
            Url = menuItem.URL;
            ParentMenuItemId = menuItem.ParentMenuItemId;
            DisplaySequence = menuItem.DisplaySequence;
            IsAlwaysEnabled = menuItem.IsAlwaysEnabled;
        }
        #endregion mehods
        #region Public Methods
        public bool HasAccessToMenu(UserAccountBO userAccount, RoleBOList roles)
        {
            if (IsAlwaysEnabled)
            {
                return true;
            }
            else
            {
                //Loop through all the roles this user is in.  The first time the user has
                //access to the menu item then return true.  If you get through all the
                //roles then the user does not have access to this menu item.
                foreach (RoleBO role in roles)
                {
                    //Check if this user is in this role
                    if (role.RoleUserAccounts.IsUserInRole(userAccount.Id))
                    {
                        //Try to find the capability with the menu item Id.
                        IEnumerable<RoleCapabilityBO> capabilities = role.RoleCapabilities.GetByMenuItemId(Id);
                        foreach (RoleCapabilityBO capability in capabilities)
                        {
                            if ((capability != null) && (capability.AccessFlag != RoleCapabilityBO.CapabiiltyAccessFlagEnum.None))
                            {
                                //If the record is in the table and the user has access other then None then return true.
                                return true;
                            }
                        }
                    }
                }
            }
            //If it gets here then the user didn't have access to this menu item.  BUT they may have access
            //to one of its children, now check the children and if they have access to any of  them  then 
            //return true.
            if (ChildMenuItems.Count > 0)
            {
                foreach (EntMenuItemBO child in ChildMenuItems)
                {
                    if (child.HasAccessToMenu(userAccount, roles))
                    {
                        return true;
                    }
                }
            }
            //If it never found a role with any capability then return false.
            return false;
        }
        #endregion Public Methods
    }
    #endregion ENTMenuItemBO
    #region ENTMenuItemBOList
    [Serializable()]
    public class EntMenuItemBOList : List<EntMenuItemBO>
    {
        #region Public Methods
        /// <summary>
        /// This will load up the object with the correct parent\child relationships within the menu structure.
        /// Any parent menu item will have its child menu items loaded in it's ChildMenuItems property.
        /// </summary>
        public void Load()
        {
            //Load the list from the database.  This will then be traversed to create the 
            //parent child relationships in for each menu item.
            List<EntMenuItem> menuItems = new EntMenuItemData().Select();
            //Traverse through the list to create the parent child relationships                                                    
            foreach (EntMenuItem menuItem in menuItems)
            {
                EntMenuItemBO menuItemBO = new EntMenuItemBO();
                menuItemBO.MapEntityToProperties(menuItem);
                // Check if the menu already exists in this object.
                if (MenuExists(menuItemBO.Id) == false)
                {
                    //Doesn't exist so now check if this is a top level item.
                    if (menuItemBO.ParentMenuItemId == null)
                    {
                        //Top level item so just add it.
                        this.Add(menuItemBO);
                    }
                    else
                    {
                        // Get the parent menu item from this object if it exists.
                        EntMenuItemBO parent = GetByMenuItemId(Convert.ToInt32(menuItemBO.ParentMenuItemId));
                        if (parent == null)
                        {
                            // If it gets here then the parent isn't in the list yet.
                            // Find the parent in the list.                            
                            EntMenuItemBO newParentMenuItem = FindOrLoadParent(menuItems, Convert.ToInt32(menuItemBO.ParentMenuItemId));
                            // Add the current child menu item to the newly added parent
                            newParentMenuItem.ChildMenuItems.Add(menuItemBO);
                        }
                        else
                        {
                            // Parent already existed in this object.
                            // Add this menu to the child of the parent
                            parent.ChildMenuItems.Add(menuItemBO);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Checks if the menu item exists.  This will look at the child object of the menu also.
        /// </summary>
        public bool MenuExists(long menuItemId)
        {
            return (GetByMenuItemId(menuItemId) != null);
        }
        /// <summary>
        /// Returns the menu item for the specified id.  This will search through all child items in the list.
        /// </summary>
        public EntMenuItemBO GetByMenuItemId(long menuItemId)
        {
            foreach (EntMenuItemBO menuItem in this)
            {
                // Check if this is the item we are looking for
                if (menuItem.Id == menuItemId)
                {
                    return menuItem;
                }
                else
                {
                    // Check if this menu has children
                    if (menuItem.ChildMenuItems.Count > 0)
                    {
                        // Search the children for this item.
                        EntMenuItemBO childMenuItem = menuItem.ChildMenuItems.GetByMenuItemId(menuItemId);
                        // If the menu is found in the children then it won't be null
                        if (childMenuItem != null)
                        {
                            return childMenuItem;
                        }
                    }
                }
            }
            //It wasn't found so return null.
            return null;
        }
        //Return the menu item that is at the top most level that does not have a parent.
        public EntMenuItemBO GetTopMenuItem(string menuItemName)
        {
            //Find the menu item by it name.
            EntMenuItemBO menuItem = GetByMenuItemName(menuItemName);
            while (menuItem.ParentMenuItemId != null)
            {
                menuItem = GetByMenuItemId(Convert.ToInt64(menuItem.ParentMenuItemId));
            }
            return menuItem;
        }
        public EntMenuItemBO GetByMenuItemName(string menuItemName)
        {
            foreach (EntMenuItemBO menuItem in this)
            {
                // Check if this is the item we are looking for
                if (menuItem.MenuItemName == menuItemName)
                {
                    return menuItem;
                }
                else
                {
                    // Check if this menu has children
                    if (menuItem.ChildMenuItems.Count > 0)
                    {
                        // Search the children for this item.
                        EntMenuItemBO childMenuItem = menuItem.ChildMenuItems.GetByMenuItemName(menuItemName);
                        // If the menu is found in the children then it won't be null
                        if (childMenuItem != null)
                        {
                            return childMenuItem;
                        }
                    }
                }
            }
            //It wasn't found so return null.
            return null;
        }
        #endregion Public Methods
        #region Private Methods
        private EntMenuItemBO FindOrLoadParent(List<EntMenuItem> menuItems, long parentMenuItemId)
        {
            // Find the menu item in the entity list.
            EntMenuItem parentMenuItem = menuItems.Single(m => m.MenuItemId == parentMenuItemId);
            // Load this into the business object.
            EntMenuItemBO menuItemBO = new EntMenuItemBO();
            menuItemBO.MapEntityToProperties(parentMenuItem);
            // Check if it has a parent
            if (parentMenuItem.ParentMenuItemId == null)
            {
                this.Add(menuItemBO);
            }
            else
            {
                // Since this has a parent it should be added to its parent's children.
                // Try to find the parent in the list already.
                EntMenuItemBO parent = GetByMenuItemId(Convert.ToInt64(parentMenuItem.ParentMenuItemId));
                if (parent == null)
                {
                    // This one's parent wasn't found.  So add it.
                    EntMenuItemBO newParent = FindOrLoadParent(menuItems, Convert.ToInt64(parentMenuItem.ParentMenuItemId));
                    newParent.ChildMenuItems.Add(menuItemBO);
                }
                else
                {
                    // Add this menu to the child of the parent
                    parent.ChildMenuItems.Add(menuItemBO);
                }
            }
            return menuItemBO;
        }
        #endregion Private Methods
    }
    #endregion ENTMenuItemBOList
}
