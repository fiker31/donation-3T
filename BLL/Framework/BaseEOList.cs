using DAL;
using System;
using System.Collections.Generic;
using System.Transactions;
namespace BLL.Framework
{
    [Serializable()]
    public abstract class BaseEOList<T> : BaseBOList<T>
        where T : BaseEO, new()
    {
        /// <summary>
        /// This method will begin a transaction and save all the edit objects in the collection.
        /// </summary>
        /// <param name="veAL"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public bool Save(ref EntValidationErrors validationErrors, string userAccountId, bool IsNewRecord)
        {
            // Check if this object has any items
            if (this.Count > 0)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (DBDataContext db = new DBDataContext())
                    {
                        if (this.Save(db, ref validationErrors, userAccountId))
                        {
                            // Commit transaction if update was successful
                            ts.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                //No items in the list so return true.
                return true;
            }
        }
        /// <summary>
        /// This method will save all the edit objects in the collection.  The transaction must be started before
        /// calling this method.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="validationErrors"></param>
        /// <param name="userAccountId"></param>
        /// <returns></returns>
        public bool Save(DBDataContext db, ref EntValidationErrors validationErrors, string userAccountId)
        {
            foreach (BaseEO genericEO in this)
            {
                if (genericEO.DBAction == BaseEO.DBActionEnum.Insert || genericEO.DBAction == BaseEO.DBActionEnum.Update)
                {
                    if (!genericEO.Save(db, ref validationErrors, userAccountId))
                    {
                        return false;
                    }
                }
                else
                {
                    if (genericEO.DBAction == BaseEO.DBActionEnum.Delete)
                    {
                        if (!genericEO.Delete(db, ref validationErrors, userAccountId))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        throw new Exception("Unknown DBAction");
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// This will load the current instance and return itself.  This is useful for object data source controls.
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            this.Load();
            return this;
        }
    }
}
