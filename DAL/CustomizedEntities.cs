using DAL.Framework;
using System.Data.Linq;

namespace DAL
{
    public partial class EntUserAccount : IBaseEntity { }
    public partial class EntRole : IBaseEntity { }
    public partial class EntCapability : IBaseEntity { }
    public partial class EntRoleCapability : IBaseEntity { }
    public partial class EntRoleUserAccount : IBaseEntity { }
    public partial class EntMenuItem : IBaseEntity { }
    public partial class BusPeriod : IBaseEntity { }
    public partial class Holiday : IBaseEntity { }
    public partial class Branch : IBaseEntity { }
    public partial class District : IBaseEntity { }
    public partial class Ticket_Basic : IBaseEntity { }
    public partial class Ticket_Category : IBaseEntity { }
    public partial class Ticket_Organizer : IBaseEntity { }
    public partial class Student : IBaseEntity { }
    public partial class Employee : IBaseEntity { }
    public partial class companyReg : IBaseEntity { }
	public partial class DonationReg_List : IBaseEntity
	{
		internal Binary poster;
	}
	public partial class Donationmade: IBaseEntity { }
    public partial class Donator_p : IBaseEntity { }
}
