using BLL.Framework;
using System.Web.Caching;
/// <summary>
/// Summary description for Globals
/// </summary>
public static class Globals
{
    #region Constants
    private const string CACHE_KEY_MENU_ITEMS = "MenuItems";
    private const string CACHE_KEY_USERS = "Users";
    private const string CACHE_KEY_ROLES = "Roles";
    private const string CACHE_KEY_CAPABILITIES = "Capabilities";
    #endregion Constants
    #region Methods
    public static EntMenuItemBOList GetMenuItems(Cache cache)
    {
        //Check if the menus have been cached.
        if (cache[CACHE_KEY_MENU_ITEMS] == null)
        {
            LoadMenuItems(cache);
        }
        return (EntMenuItemBOList)cache[CACHE_KEY_MENU_ITEMS];
    }
    public static UserAccountBOList GetUsers(Cache cache)
    {
        //Check for the users
        if (cache[CACHE_KEY_USERS] == null)
        {
            LoadUsers(cache);
        }
        return (UserAccountBOList)cache[CACHE_KEY_USERS];
    }
    public static RoleBOList GetRoles(Cache cache)
    {
        //Check for the roles
        if (cache[CACHE_KEY_ROLES] == null)
        {
            LoadRoles(cache);
        }
        return (RoleBOList)cache[CACHE_KEY_ROLES];
    }
    public static CapabilityBOList GetCapabilities(Cache cache)
    {
        //Check for the roles
        if (cache[CACHE_KEY_CAPABILITIES] == null)
        {
            LoadCapabilities(cache);
        }
        return (CapabilityBOList)cache[CACHE_KEY_CAPABILITIES];
    }
    public static void LoadMenuItems(Cache cache)
    {
        EntMenuItemBOList menuItems = new EntMenuItemBOList();
        menuItems.Load();
        cache.Remove(CACHE_KEY_MENU_ITEMS);
        cache[CACHE_KEY_MENU_ITEMS] = menuItems;
    }
    public static void LoadUsers(Cache cache)
    {
        UserAccountBOList users = new UserAccountBOList();
        users.LoadWithRoles();
        cache.Remove(CACHE_KEY_USERS);
        cache[CACHE_KEY_USERS] = users;
    }
    public static void LoadRoles(Cache cache)
    {
        RoleBOList roles = new RoleBOList();
        roles.Load();
        cache.Remove(CACHE_KEY_ROLES);
        cache[CACHE_KEY_ROLES] = roles;
    }
    public static void LoadCapabilities(Cache cache)
    {
        CapabilityBOList capabilities = new CapabilityBOList();
        capabilities.Load();
        cache.Remove(CACHE_KEY_CAPABILITIES);
        cache[CACHE_KEY_CAPABILITIES] = capabilities;
    }
    #endregion Methods
}
