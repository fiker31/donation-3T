using System.Collections.Generic;
using System.Linq;
namespace DAL.Framework
{
    public class EntMenuItemData
    {
        public List<EntMenuItem> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<EntMenuItem> MI = (from mi in db.EntMenuItems
                                        orderby mi.ParentMenuItemId ascending, mi.DisplaySequence ascending, mi.MenuItemName ascending
                                        select mi).ToList();
                return MI;
            }
        }
    }
}
