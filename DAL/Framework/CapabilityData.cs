using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
namespace DAL.Framework
{
    public class CapabilityData : BaseData<EntCapability>
    {
        #region Overrides
        public override List<EntCapability> Select()
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                List<EntCapability> C = (from c in db.EntCapabilities
                                         select c).ToList();
                return C;
            }
        }
        public override EntCapability Select(long id)
        {
            using (DBDataContext db = new DBDataContext(DBHelper.GetCreditDBConnectionString()))
            {
                var C = (from c in db.EntCapabilities
                         where c.CapabilityId == id
                         select c);
                if (C.Count() != 0)
                {
                    return C.Single();
                }
                else
                {
                    return null;
                }
            }
        }
        public override EntCapability Select(string id)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, long id, Binary version)
        {
            throw new NotImplementedException();
        }
        public override void Delete(DBDataContext db, string id, Binary version)
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
    }
}
