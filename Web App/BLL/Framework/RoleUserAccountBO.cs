using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Framework;
namespace BLL.Framework
{
    #region RoleUserAccountBO
    [Serializable()]
    public class RoleUserAccountBO : BaseEO
    {
        #region Properties
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long UserAccountId { get; set; }
        #endregion Properties
        #region Overrides
        public override bool Save(AppDataContext db, ref EntValidationErrors validationErrors, string userAccountId)
        {
            if (DBAction == DBActionEnum.Insert || DBAction == DBActionEnum.Update)
            {
                //Validate the object
                Validate(db, ref validationErrors);
                //Check if there were any validation errors
                if (validationErrors.Count == 0)
                {
                    if (DBAction == DBActionEnum.Insert)
                    {
                        //Add
                        Id = new RoleUserAccountData().Insert(db, RoleId, UserAccountId, userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new RoleUserAccountData().Update(db, Id, RoleId, UserAccountId, InsertUserId, EntryDate, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    //Didn't pass validation.
                    return false;
                }
            }
            else
            {
                throw new Exception("DBAction not save.");
            }
        }
        protected override void Validate(AppDataContext db, ref EntValidationErrors validationErrors)
        {
            //None
        }
        protected override void DeleteForReal(AppDataContext db)
        {
            new RoleUserAccountData().Delete(db, Id, Version);
        }
        protected override void ValidateDelete(AppDataContext db, ref EntValidationErrors validationErrors)
        {
            //None
        }
        public override void Init()
        {
            throw new NotImplementedException();
        }
        public override bool Load(long id)
        {
            throw new NotImplementedException();
        }
        protected override void MapEntityToCustomProperties(IBaseEntity entity)
        {
            EntRoleUserAccount roleUserAccount = (EntRoleUserAccount)entity;
            Id = roleUserAccount.RoleUserAccountId;
            RoleId = roleUserAccount.RoleId;
            UserAccountId = roleUserAccount.UserAccountId;
        }
        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }
        #endregion Overrides
        public override bool Load(string id)
        {
            throw new NotImplementedException();
        }
    }
    #endregion RoleUserAccountBO
    #region RoleUserAccountBOList
    [Serializable()]
    public class RoleUserAccountBOList : BaseEOList<RoleUserAccountBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new RoleUserAccountData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<EntRoleUserAccount> roleUserAccounts)
        {
            if (roleUserAccounts.Count > 0)
            {
                foreach (EntRoleUserAccount roleUserAccount in roleUserAccounts)
                {
                    RoleUserAccountBO newRoleUserAccountEO = new RoleUserAccountBO();
                    newRoleUserAccountEO.MapEntityToProperties(roleUserAccount);
                    this.Add(newRoleUserAccountEO);
                }
            }
        }
        #endregion Private Methods
        #region Public Methods
        public bool IsUserInRole(long userAccountId)
        {
            return (GetByUserAccountId(userAccountId) != null);
        }
        /// <summary>
        /// Return the object from this instance with the specified userAccountId
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <returns></returns>
        public RoleUserAccountBO GetByUserAccountId(long userAccountId)
        {
            return this.SingleOrDefault(u => u.UserAccountId == userAccountId);
        }
        /// <summary>
        /// Load the current instance by role id
        /// </summary>
        /// <param name="roleID"></param>
        internal void LoadByRoleId(long roleID)
        {
            LoadFromList(new RoleUserAccountData().SelectByRoleId(roleID));
        }
        #endregion Public Methods
    }
    #endregion RoleUserAccountBOList
}
