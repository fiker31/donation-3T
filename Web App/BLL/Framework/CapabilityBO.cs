using DAL;
using DAL.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BLL.Framework
{
    #region CapabilityBO
    [Serializable()]
    public class CapabilityBO : BaseBO
    {
        #region Enumerations
        public enum AccessTypeEnum
        {
            ReadOnlyEditDelete = 0,
            ReadOnlyEdit = 1,
            ReadOnly = 2,
            Edit = 3
        }
        #endregion Enumerations
        #region Properties
        public long Id { get; set; }
        public string CapabilityName { get; private set; }
        public Nullable<long> MenuItemId { get; private set; }
        public AccessTypeEnum AccessType { get; private set; }
        #endregion Properties
        #region Overrides
        public override bool Load(long id)
        {
            EntCapability capability = new CapabilityData().Select(id);
            MapEntityToProperties(capability);
            return true;
        }
        protected override void MapEntityToCustomProperties(IBaseEntity entity)
        {
            EntCapability capability = (EntCapability)entity;
            Id = capability.CapabilityId;
            CapabilityName = capability.CapabilityName;
            MenuItemId = capability.MenuItemId;
            AccessType = (AccessTypeEnum)capability.AccessType;
        }
        protected override string GetDisplayText()
        {
            return CapabilityName;
        }
        #endregion Overrides
        public override bool Load(string id)
        {
            throw new NotImplementedException();
        }
    }
    #endregion CapabilityBO
    #region CapabilityList
    [Serializable()]
    public class CapabilityBOList : BaseBOList<CapabilityBO>
    {
        #region Overrides
        public override void Load()
        {
            LoadFromList(new CapabilityData().Select());
        }
        #endregion Overrides
        #region Private Methods
        private void LoadFromList(List<EntCapability> capabilities)
        {
            if (capabilities.Count > 0)
            {
                foreach (EntCapability capability in capabilities)
                {
                    CapabilityBO newCapabilityBO = new CapabilityBO();
                    newCapabilityBO.MapEntityToProperties(capability);
                    this.Add(newCapabilityBO);
                }
            }
        }
        #endregion Private Methods
        #region Public Methods
        /// <summary>
        /// Returns a Capability with the matching name.  If it is not found then null is returned.
        /// </summary>
        /// <param name="capabilityName"></param>
        /// <returns></returns>
        public CapabilityBO GetByName(string capabilityName)
        {
            return this.SingleOrDefault(c => c.CapabilityName == capabilityName);
        }
        #endregion Public Methods
        public IEnumerable<CapabilityBO> GetByMenuItemId(long menuItemId)
        {
            return from c in this
                   where c.MenuItemId == menuItemId
                   orderby c.CapabilityName
                   select c;
        }
    }
    #endregion CapabilityList
}
