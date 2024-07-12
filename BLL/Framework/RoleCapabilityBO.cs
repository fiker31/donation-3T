using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Framework;
namespace BLL.Framework
{
    #region RoleCapabilityBO
    [Serializable()]
    public class RoleCapabilityBO : BaseEO
    {
        #region Enumerations
        public enum CapabiiltyAccessFlagEnum
        {
            None,
            ReadOnly,
            Edit,
            EditWithDelete
        }
        #endregion Enumerations
        #region Constructor
        public RoleCapabilityBO()
        {
            Capability = new CapabilityBO();
        }
        #endregion Constructor
        #region Properties
        public long Id { get; set; }
        public long RoleId { get; set; }
        public CapabiiltyAccessFlagEnum AccessFlag { get; set; }
        public CapabilityBO Capability { get; private set; }
        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            EntRoleCapability roleCapability = new RoleCapabilityData().Select(id);
            MapEntityToProperties(roleCapability);
            return true;
        }
        protected override void MapEntityToCustomProperties(IBaseEntity entity)
        {
            EntRoleCapability roleCapability = (EntRoleCapability)entity;
            Id = roleCapability.RoleCapabilityId;
            RoleId = roleCapability.RoleId;
            AccessFlag = (CapabiiltyAccessFlagEnum)roleCapability.AccessFlag;
            Capability.Load(roleCapability.CapabilityId);
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
                        Id = new RoleCapabilityData().Insert(db, RoleId, Capability.Id, Convert.ToByte(AccessFlag), userAccountId);
                    }
                    else
                    {
                        //Update
                        if (!new RoleCapabilityData().Update(db, Id, RoleId, Capability.Id, Convert.ToByte(AccessFlag), InsertUserId, EntryDate, userAccountId, Version))
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
        protected override void Validate(DBDataContext db, ref EntValidationErrors validationErrors)
        {
            //No validation
        }
        protected override void DeleteForReal(DBDataContext db)
        {
            new RoleCapabilityData().Delete(db, Id, Version);
        }
        protected override void ValidateDelete(DBDataContext db, ref EntValidationErrors validationErrors)
        {
            //No validation
        }
        public override void Init()
        {
            //No defaults
        }
        protected override string GetDisplayText()
        {
            //never should happen since this is a junction table.
            throw new NotImplementedException();
        }
        #endregion Overrides
        public override bool Load(string id)
        {
            throw new NotImplementedException();
        }
    }
    #endregion RoleCapabilityBO
    #region RoleCapabilityBOList
    [Serializable()]
    public class RoleCapabilityBOList : BaseEOList<RoleCapabilityBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new RoleCapabilityData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<EntRoleCapability> roleCapabilities)
        {
            if (roleCapabilities.Count > 0)
            {
                foreach (EntRoleCapability roleCapability in roleCapabilities)
                {
                    RoleCapabilityBO newRoleCapabilityEO = new RoleCapabilityBO();
                    newRoleCapabilityEO.MapEntityToProperties(roleCapability);
                    this.Add(newRoleCapabilityEO);
                }
            }
        }
        #endregion Private Methods
        #region Internal Methods
        /// <summary>
        /// Returns all the Role\Capabilitis for the specificed menuItemId.
        /// </summary>
        /// <param name="menuItemId"></param>
        /// <returns></returns>
        internal IEnumerable<RoleCapabilityBO> GetByMenuItemId(long menuItemId)
        {
            return from rc in this
                   where rc.Capability.MenuItemId == menuItemId
                   select rc;
        }
        /// <summary>
        /// Load this instance by role id.
        /// </summary>
        /// <param name="roleId"></param>
        internal void LoadByRoleId(long roleId)
        {
            LoadFromList(new RoleCapabilityData().SelectByRoleId(roleId));
        }
        #endregion Internal Methods
        #region Public Methods
        /// <summary>
        /// Get from this instance the object with the specified capability.
        /// </summary>
        /// <param name="capabilityId"></param>
        /// <returns></returns>
        public RoleCapabilityBO GetByCapabilityID(long capabilityId)
        {
            return this.SingleOrDefault(rc => rc.Capability.Id == capabilityId);
        }
        #endregion Public Methods
    }
    #endregion RoleCapabilityBOList
}
