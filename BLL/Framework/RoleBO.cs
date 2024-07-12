using DAL;
using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BLL.Framework
{
    #region RoleBO
    [Serializable()]
    public class RoleBO : BaseEO
    {
        #region Constructor
        public RoleBO()
        {
            RoleCapabilities = new RoleCapabilityBOList();
            RoleUserAccounts = new RoleUserAccountBOList();
        }
        #endregion Constructor
        #region Properties
        public long Id { get; set; }
        public string RoleName { get; set; }
        public RoleCapabilityBOList RoleCapabilities { get; private set; }
        public RoleUserAccountBOList RoleUserAccounts { get; private set; }
        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            //Get a data reader from the database.
            EntRole role = new RoleData().Select(id);
            MapEntityToProperties(role);
            return true;
        }
        protected override void MapEntityToCustomProperties(IBaseEntity entity)
        {
            EntRole role = (EntRole)entity;
            Id = role.RoleId;
            RoleName = role.RoleName;
            RoleCapabilities = new RoleCapabilityBOList();
            RoleUserAccounts = new RoleUserAccountBOList();
            RoleCapabilities.LoadByRoleId(Id);
            RoleUserAccounts.LoadByRoleId(Id);
        }
        public override bool Save(DBDataContext db, ref EntValidationErrors validationErrors, string userAccountId)
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
                        Id = new RoleData().Insert(db, RoleName, userAccountId);
                        //Since this was an add you need to update all the role ids for the user and capability records
                        foreach (RoleCapabilityBO capability in RoleCapabilities)
                        {
                            capability.RoleId = Id;
                        }
                        foreach (RoleUserAccountBO user in RoleUserAccounts)
                        {
                            user.RoleId = Id;
                        }
                    }
                    else
                    {
                        //Update
                        if (!new RoleData().Update(db, Id, RoleName, InsertUserId, EntryDate, userAccountId, Version))
                        {
                            UpdateFailed(ref validationErrors);
                            return false;
                        }
                    }
                    //Now save the capabilities
                    if (RoleCapabilities.Save(db, ref validationErrors, userAccountId))
                    {
                        //Now save the users
                        if (RoleUserAccounts.Save(db, ref validationErrors, userAccountId))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    //return true;
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
        protected override void Validate(DBDataContext db, ref EntValidationErrors validationErrors)
        {
            if (RoleName.Trim().Length == 0)
            {
                validationErrors.Add("The name is required.");
            }
            //The role name must be unique.
            if (new RoleData().IsDuplicateRoleName(db, Id, RoleName))
            {
                validationErrors.Add("The name must be unique.");
            }
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new RoleData().Delete(db, Id, Version);
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
            //No Validation
        }
        public override void Init()
        {
            //Nothing to default
        }
        protected override string GetDisplayText()
        {
            return RoleName;
        }
        #endregion Overrides
        public override bool Load(string id)
        {
            throw new NotImplementedException();
        }
    }
    #endregion RoleBO
    #region RoleBOList
    [Serializable()]
    public class RoleBOList : BaseEOList<RoleBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new RoleData().Select());
        }
        #endregion Overrides
        public void LoadByRoleName(string roleName)
        {
            LoadFromList(new RoleData().SelectByRoleName(roleName));
        }
        #region Private Methods
        private void LoadFromList(List<EntRole> roles)
        {
            if (roles.Count > 0)
            {
                foreach (EntRole role in roles)
                {
                    RoleBO newRoleEO = new RoleBO();
                    newRoleEO.MapEntityToProperties(role);
                    this.Add(newRoleEO);
                }
            }
        }
        #endregion Private Methods
        #region Internal Methods
        internal RoleBO GetByRoleId(long roleId)
        {
            return this.SingleOrDefault(r => r.Id == roleId);
        }
        internal void LoadByUserAccountId(long userAccountId)
        {
            LoadFromList(new RoleData().SelectByUserAccountId(userAccountId));
        }
        #endregion Internal Methods
    }
    #endregion RoleBOList
}
